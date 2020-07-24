using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using SWMproject.Data;
using System.Net;
using System.Net.Sockets;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Diagnostics;

namespace SWMproject.Dialogs
{
    public class KeywordOrderDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;
        private static Database database = null;
        private static Container container = null;
        private static readonly string databaseId = Startup.DatabaseId;
        private static readonly string containerId = Startup.ContainerId_keyword;

        private static string ipAddr;
        private static List<DBdata> keywordList;

        private static int flag;
        private static Sandwich my_sandwich;

        //키워드 주문하기 
        public KeywordOrderDialog(UserState userState) : base(nameof(KeywordOrderDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");
            flag = 0;
            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                //키워드 확인? 키워드 입력? 키워드 삭제?
                SelectStepAsync,
                BeginKeywordStepAsync,
                //키워드 입력
                EnterKeywordStepAsync,
                KeywordOrderStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new OrderDialog(userState));
            AddDialog(new OrderEndDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        private static async Task<DialogTurnResult> SelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("무엇을 도와드릴까요?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "키워드 확인하기", "키워드 주문하기","키워드 삭제하기","키워드 만들고 주문하기" }),
               }, cancellationToken);
        }
        private static async Task<DialogTurnResult> BeginKeywordStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string result = ((FoundChoice)stepContext.Result).Value;
            if (result == "키워드 만들고 주문하기")
            {
                return await stepContext.ReplaceDialogAsync(nameof(OrderDialog), null, cancellationToken);
            }

            await Initialize();
            if (result == "키워드 확인하기")
            {
                keywordList = new List<DBdata>();
                await GetAllKeyword(ipAddr);
                //키워드 출력
                string list = "키워드 리스트입니다!\r\n";
                foreach(var kw in keywordList)
                {
                    list += kw.id;
                    list += " ";
                }
                if(keywordList.Count<1)
                    await stepContext.Context.SendActivityAsync("키워드가 없습니다!");
                else
                    await stepContext.Context.SendActivityAsync(list);
                return await stepContext.ReplaceDialogAsync(nameof(KeywordOrderDialog),null, cancellationToken);
            }
            else 
            { 
                if(result =="키워드 주문하기")
                {
                    flag = 1;
                }
                else if(result =="키워드 삭제하기")
                {
                    flag = 2;
                }
                
            }    
            return await stepContext.NextAsync(null, cancellationToken);
        }
        private static async Task<DialogTurnResult> EnterKeywordStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("키워드를 입력하세요.") }, cancellationToken);
        }
        private async Task<DialogTurnResult> KeywordOrderStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string keyword = stepContext.Result.ToString();
            await GetKeyword(ipAddr,keyword);
            if (my_sandwich == null)
            {
                await stepContext.Context.SendActivityAsync("키워드가 없습니다!");
                return await stepContext.ReplaceDialogAsync(nameof(KeywordOrderDialog), null, cancellationToken);
            }

            
            switch (flag)
            {
                case 1: //키워드 주문하기
                    var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
                    orderData.Price += Topping.menu_price[my_sandwich.Menu];
                    foreach(var top in my_sandwich.Topping)
                    {
                        orderData.Price += Topping.topping_price[top];
                    }
                    orderData.Num++;
                    orderData.Sandwiches.Add(my_sandwich);
                    return await stepContext.ReplaceDialogAsync(nameof(OrderEndDialog), null, cancellationToken);
                case 2: //키워드 삭제하기
                    try
                    {
                        ItemResponse<DBdata> item = await container.DeleteItemAsync<DBdata>(keyword, new PartitionKey(ipAddr));
                    }
                    catch(System.Exception e)
                    {
                        Debug.Print(e.Message);
                    }
                    return await stepContext.ReplaceDialogAsync(nameof(KeywordOrderDialog), null, cancellationToken);
                default:
                    break;
            }
            //return await stepContext.ReplaceDialogAsync(nameof(MenuDialog), null, cancellationToken);
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        
        private static async Task Initialize()
        {
            //db 가져오기
            CosmosClient client = new CosmosClient(Startup.CosmosDbEndpoint, Startup.AuthKey);
            database = await client.CreateDatabaseIfNotExistsAsync(databaseId);

            ContainerProperties containerProperties = new ContainerProperties(containerId, partitionKeyPath: Startup.PartitionKey);
            container = await database.CreateContainerIfNotExistsAsync(
                containerProperties,
                throughput: 1000);

            //ip
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            ipAddr = "";
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = host.AddressList[i].ToString();
                }
            }
        }
        private static async Task GetAllKeyword(string ip)
        {
            //keyword 전체 가져오기
            try
            {
                FeedIterator<DBdata> feedIterator = container.GetItemQueryIterator<DBdata>("SELECT * FROM c WHERE c.AccountNumber ='"+ip+"'");
                {
                    while (feedIterator.HasMoreResults)
                    {
                        FeedResponse<DBdata> result = await feedIterator.ReadNextAsync();
                        foreach (var item in result)
                        {
                            keywordList.Add(item);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Print(e.Message);
            }
        }
        private static async Task GetKeyword(string ip, string keyword)
        {
            //keyword 가져오기
            try
            {
                FeedIterator<DBdata> feedIterator = container.GetItemQueryIterator<DBdata>("SELECT * FROM c WHERE c.id='"+keyword+ "' and c.AccountNumber ='" + ip + "'");
                {
                    while (feedIterator.HasMoreResults)
                    {
                        FeedResponse<DBdata> result = await feedIterator.ReadNextAsync();
                        foreach (var item in result)
                        {
                            dynamic sandwich = item.Contents;
                            my_sandwich = new Sandwich(sandwich.Menu, sandwich.Bread, sandwich.Cheese, sandwich.Warmup, sandwich.Topping, sandwich.Vege, sandwich.Sauce, sandwich.SetMenu, sandwich.SetDrink);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Print(e.Message);
            }
        }

    }
}
