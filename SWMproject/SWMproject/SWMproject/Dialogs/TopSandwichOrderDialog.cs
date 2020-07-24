using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using SWMproject.Data;
using System.Net;
using System.Net.Sockets;
using System;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using System.Reflection.Metadata;

namespace SWMproject.Dialogs
{
    public class TopSandwichOrderDialog : ComponentDialog
    {
        private static Database database = null;
        private static Container container = null;
        private static readonly string databaseId = "test";
        private static readonly string containerId = "Count";

        public TopSandwichOrderDialog(UserState userState) : base(nameof(TopSandwichOrderDialog))
        {
            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                ShowRankingStepAsync,
                ResponceStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new OrderDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        private static async Task<DialogTurnResult> ShowRankingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            CosmosClient client = new CosmosClient("https://sandwichmaker-db.documents.azure.com:443/", "a9myphpBRmWUJ5ZLKdCiVEODOtSkiOWr66uKWOCyGljEo2C6Vru1qZ6V4vmXH8VUrij3zriZlQ93xIU4vlZlzA==");
            database = await client.CreateDatabaseIfNotExistsAsync(databaseId);

            ContainerProperties containerProperties = new ContainerProperties(containerId, partitionKeyPath: "/AccountNumber");
            // Create with a throughput of 1000 RU/s
            container = await database.CreateContainerIfNotExistsAsync(
                containerProperties,
                throughput: 1000);

            try
            {
                FeedIterator<DBcount> test = container.GetItemQueryIterator<DBcount>("SELECT top 5 * FROM c order by c.Count desc");
                {
                    while (test.HasMoreResults)
                    {
                        var result = await test.ReadNextAsync();
                        var topMsg = ""; int ranking = 1;
                        foreach (DBcount item in result)
                        {
                            topMsg += "[TOP" + ranking.ToString() + "]\r\n";
                            topMsg += "빵: " + item.Bread + "\r\n";
                            topMsg += "메뉴: " + item.Menu+"\r\n";
                            topMsg += "소스: " + item.Sauce + "\r\n\r\n";
                            ranking++;
                        }
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{topMsg}"), cancellationToken);
                    }
                }
            }
            catch (System.Exception e)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{e}"), cancellationToken);
            }

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("주문하시겠습니까?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오" }),
               }, cancellationToken);
        }
        private static async Task<DialogTurnResult> ResponceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "네")
            {
                return await stepContext.BeginDialogAsync(nameof(OrderDialog), null, cancellationToken);
            }
            else return await stepContext.EndDialogAsync();
        }
    }
}
