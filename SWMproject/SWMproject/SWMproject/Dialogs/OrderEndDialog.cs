using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using SWMproject.Data;

namespace SWMproject.Dialogs
{
    public class OrderEndDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;
        public OrderEndDialog(UserState userState) : base(nameof(OrderEndDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");

            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                SummaryStepAsync
            };
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            orderData.OrderNum++;
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
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
