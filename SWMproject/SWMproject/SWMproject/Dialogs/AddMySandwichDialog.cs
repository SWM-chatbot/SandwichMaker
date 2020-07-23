using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using SWMproject.Data;

namespace SWMproject.Dialogs
{
    public class AddMySandwichDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;
        private static Database database = null;
        private static Container container = null;
        private static readonly string databaseId = "test";
        private static readonly string containerId = "MySandwiches";
        public AddMySandwichDialog(UserState userState) : base(nameof(AddMySandwichDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");
            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                KeywordStepAsync,
                InsertDBStepAsync
            };
            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new OrderEndDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        private async Task<DialogTurnResult> KeywordStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("조합 애칭을 무엇으로 할까요? \r\n애칭을 통해 간편히 나만의 샌드위치를 주문할 수 있어요!") };
            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }
        private async Task<DialogTurnResult> InsertDBStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            CosmosClient client = new CosmosClient("https://sandwichmaker-db.documents.azure.com:443/", "a9myphpBRmWUJ5ZLKdCiVEODOtSkiOWr66uKWOCyGljEo2C6Vru1qZ6V4vmXH8VUrij3zriZlQ93xIU4vlZlzA==");
            database = await client.CreateDatabaseIfNotExistsAsync(databaseId);

            ContainerProperties containerProperties = new ContainerProperties(containerId, partitionKeyPath: "/AccountNumber");
            // Create with a throughput of 1000 RU/s
            container = await database.CreateContainerIfNotExistsAsync(
                containerProperties,
                throughput: 1000);

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            var keyword = (string)stepContext.Result;
            var sandwich = orderData.Sandwiches.Last();

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddr = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = host.AddressList[i].ToString();
                }
            }

            string sJson = JsonConvert.SerializeObject(sandwich);
            var dbData = new DBdata { id = $"{keyword}", Contents = sandwich, ETag = "x", AccountNumber = ipAddr };
            await container.UpsertItemAsync<DBdata>(dbData, new PartitionKey(dbData.AccountNumber));

            return await stepContext.BeginDialogAsync(nameof(OrderEndDialog));
        }
    }
}