using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using SWMproject.Data;
using System.Net;
using System.Net.Sockets;
using System;

namespace SWMproject.Dialogs
{
    public class OrderEndDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;
        private static Database database = null;
        private static Container container = null;
        private static readonly string databaseId = "test";
        private static readonly string containerId = "Order";
        public OrderEndDialog(UserState userState) : base(nameof(OrderEndDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");

            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                SummaryStepAsync,
                DataUpdateStepAsync
            };
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            List<ReceiptItem> ItemList = new List<ReceiptItem> { new ReceiptItem(image: new CardImage(url: "https://www.subway.co.kr/images/common/logo_w.png")) };
            ItemList.Add(new ReceiptItem("-----------------------------------"));

            foreach (Sandwich tmpSand in orderData.Sandwiches)
            {
                ItemList.Add(new ReceiptItem("메뉴", price: Topping.menu_price[tmpSand.Menu].ToString() + "원"));
                foreach (string temp in tmpSand.Topping)
                {
                    ItemList.Add(new ReceiptItem(temp, price: Topping.topping_price[temp].ToString() + "원"));
                }
                if (orderData.SetMenu != "단품")
                {
                    ItemList.Add(new ReceiptItem("세트", subtitle: $"{tmpSand.SetMenu},{tmpSand.SetDrink}", price: "1900원"));
                    orderData.Price += 1900;
                }
                string vege = "";
                string sauce = "";
                foreach (string temp in tmpSand.Vege)
                {
                    vege += temp + ",";
                }
                foreach (string temp in tmpSand.Sauce)
                {
                    sauce += temp + ",";
                }
                ItemList.Add(new ReceiptItem("야채", subtitle: vege));
                ItemList.Add(new ReceiptItem("소스", subtitle: sauce));
                ItemList.Add(new ReceiptItem("-----------------------------------"));
            }

            ItemList.Add(new ReceiptItem("요구사항", subtitle: $"{orderData.Requirement}"));
            ItemList.Add(new ReceiptItem("총 주문 개수", price: orderData.Num.ToString() + "개"));
            ItemList.Add(new ReceiptItem("==========================="));

            var receiptCard = new ReceiptCard
            {
                Title = "Subway receipt",
                Facts = new List<Fact> { new Fact("주문번호", $"{orderData.OrderNum }"), new Fact("주문지점", $"{orderData.location}") }, //order number DB 연결?
                Items = ItemList,
                Total = $"{orderData.Price}원"
            };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문 내역을 확인해주세요."), cancellationToken);
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(receiptCard.ToAttachment()), cancellationToken);

            orderData.Initial = true;
            return await stepContext.NextAsync();
        }

        private async Task<DialogTurnResult> DataUpdateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            CosmosClient client = new CosmosClient("https://sandwichmaker-db.documents.azure.com:443/", "a9myphpBRmWUJ5ZLKdCiVEODOtSkiOWr66uKWOCyGljEo2C6Vru1qZ6V4vmXH8VUrij3zriZlQ93xIU4vlZlzA==");
            database = await client.CreateDatabaseIfNotExistsAsync(databaseId);

            ContainerProperties containerProperties = new ContainerProperties(containerId, partitionKeyPath: "/AccountNumber");
            // Create with a throughput of 1000 RU/s
            container = await database.CreateContainerIfNotExistsAsync(
                containerProperties,
                throughput: 1000);

            //orderNum 가져오기
            try
            {
                FeedIterator<DBdata> feedIterator = container.GetItemQueryIterator<DBdata>("SELECT top 1 * FROM c order by c._ts desc");
                {
                    while (feedIterator.HasMoreResults)
                    {
                        FeedResponse<DBdata> result = await feedIterator.ReadNextAsync();
                        foreach (var item in result)
                        {
                            orderData.OrderNum = Int32.Parse(item.id) + 1;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                orderData.OrderNum = 0;
            }

            //ip
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddr = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = host.AddressList[i].ToString();
                }
            }

            //한사람이 여러개를 주문한 경우, 각각의 샌드위치마다 ID값을 증가시키며 데이터 삽입
            foreach (Sandwich tempSand in orderData.Sandwiches)
            {
                string sJson = JsonConvert.SerializeObject(tempSand);
                var dbData = new DBdata { id = $"{orderData.OrderNum++}", Contents = tempSand, ETag = "x", AccountNumber = ipAddr };
                
                await container.CreateItemAsync<DBdata>(dbData, new PartitionKey(dbData.AccountNumber));
            }

            //OrderNum 데이터 업데이트 (삭제 후 새로 삽입)
            //ItemResponse<OrderNumber> response = await container.DeleteItemAsync<OrderNumber>(
            //partitionKey: new PartitionKey("0"),
            //id: "OrderNum");
            //OrderNumber orderNumber = new OrderNumber(orderData.OrderNum);
            //await container.CreateItemAsync(orderNumber, new PartitionKey("0"));

            return await stepContext.EndDialogAsync();
        }
    }
}
