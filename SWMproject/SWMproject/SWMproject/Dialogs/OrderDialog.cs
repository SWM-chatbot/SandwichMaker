using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using SWMproject.Data;

namespace SWMproject.Dialogs
{
    public class OrderDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;

        public OrderDialog(UserState userState) : base(nameof(OrderDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");

            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                //2 빵
                BreadStepAsync,
                //3 메뉴
                MenuStepAsync,
                //4 토핑 카드 보여주기
                ShowToppingStepAsync,
                // 빵 데우기
                WarmupStepAsync,
                //8 세트 선택
                SetMenuStepAsync,
                SetMenuAddiStepAsync,
                SetMenuDrinkStepAsync,
                //단품 추가
                AddiStepAsync,
                //9 추가 요구사항
                RequirementStepAsync,
                //10 주문내역
                SummaryStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new AddToppingDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        //async 정의
        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            if (orderData.Initial)
            {
                orderData.Num = 0;
                orderData.Sandwiches = new List<Sandwich>();
                orderData.Price = 0;
                orderData.Initial = false;

                return await stepContext.NextAsync();
            }
            else return await stepContext.NextAsync();
        }
        private static async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["bread"] = ((FoundChoice)stepContext.Result).Value;
            
            await stepContext.Context.SendActivityAsync(Cards.GetCard("menu"), cancellationToken);


            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("메뉴를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "쉬림프", "로티세리 바비큐 치킨", "폴드포크", "에그마요","이탈리안 비엠티","비엘티","미트볼","햄","참치","로스트 치킨","터키","베지","써브웨이 클럽","스테이크 & 치즈","스파이시 이탈리안","치킨 데리야끼" }),
               }, cancellationToken);
        }
        
        private static async Task<DialogTurnResult> BreadStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync(Cards.GetCard("bread"), cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "허니오트", "하티", "파마산 오레가노", "화이트","플랫브래드","위트" }),
               }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowToppingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            stepContext.Values["menu"] = ((FoundChoice)stepContext.Result).Value;

            if (orderData.Initial)
            {
                //치즈 카드 보여주기
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("치즈 종류입니다"), cancellationToken);
                await stepContext.Context.SendActivityAsync(Cards.GetCard("cheese"), cancellationToken);

                //소스 카드 보여주기
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("소스 종류입니다"), cancellationToken);
                await stepContext.Context.SendActivityAsync(Cards.GetCard("sauce"), cancellationToken);

                //추가 토핑 카드 보여주기
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("추가 토핑 종류입니다"), cancellationToken);
                await stepContext.Context.SendActivityAsync(Cards.GetCard("topping"), cancellationToken);

                //입력 tip 
                var tipMsg = MessageFactory.Text("[입력 TIP] \r\n- 기본적으로 모든 야채가 추가되어 있습니다.\r\n- 제외할 토핑은 '-'(빼기)와 토핑이름을 입력하면 추가되어 있던 토핑이 빠집니다.\r\n- 많이 넣고 싶은 토핑은 토핑이름을 입력하면 토핑이 추가됩니다.\r\n- '토핑종류'를 입력하면 토핑 카드를 다시 보여줍니다.\r\n- '완성'을 입력하면 토핑추가가 종료됩니다.\r\n- '?','가이드','help'를 입력하면 입력 TIP이 다시 출력됩니다.");
                await stepContext.Context.SendActivityAsync(tipMsg, cancellationToken);
            }

            orderData.Menu = (string)stepContext.Values["menu"];
            orderData.Price += Topping.menu_price[orderData.Menu];
            orderData.Bread = (string)stepContext.Values["bread"];
            orderData.Vege = new List<string> { "토마토", "올리브", "양상추", "양파", "파프리카", "오이", "피망", "피클", "할라피뇨" };
            orderData.Cheese = new List<string>();
            orderData.Sauce = new List<string>();
            orderData.SetMenu = "단품";
            orderData.SetDrink = "-";
            orderData.Topping = new List<string>();
            orderData.AddiOrder = new List<string>();

            return await stepContext.BeginDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        
        }

        private static async Task<DialogTurnResult> WarmupStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 데울까요?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네","아니오" }),
               }, cancellationToken);
        }

        private static async Task<DialogTurnResult> SetMenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["warmup"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text(" 1900원 추가 지불하고 세트로 주문하시겠습니까?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오" }),
               }, cancellationToken);
        }
        private static async Task<DialogTurnResult> SetMenuAddiStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["setmenu"] = "단품";
            if (((FoundChoice)stepContext.Result).Value == "네")
            {
                stepContext.Values["setmenu"] = "세트";

                await stepContext.Context.SendActivityAsync(Cards.GetCard("cookie"), cancellationToken);

                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                   Prompt = MessageFactory.Text("세트 메뉴를 골라주세요"),
                   Choices = ChoiceFactory.ToChoices(new List<string> {"더블 초코칩쿠키","초코칩쿠키","오트밀 레이즌쿠키","라즈베리 치즈케익쿠키","화이트 초코 마카다미아쿠키","칩"}),
                }, cancellationToken);
            }

            return await stepContext.NextAsync();
        }
        private static async Task<DialogTurnResult> SetMenuDrinkStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Values["setmenu"] == "단품")
            {
                return await stepContext.NextAsync();
            }
            stepContext.Values["setmenu"] = ((FoundChoice)stepContext.Result).Value;
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("세트 음료를 골라주세요"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "콜라","사이다","닥터 페퍼" }),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> AddiStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            if ((string)stepContext.Values["setmenu"] != "단품")
            {
                stepContext.Values["setdrink"] = ((FoundChoice)stepContext.Result).Value;
                orderData.SetDrink = (string)stepContext.Values["setdrink"];
            }

            Sandwich sandwich = new Sandwich(orderData.Menu,orderData.Bread,orderData.Cheese,orderData.Warmup,orderData.Topping,orderData.Vege,orderData.Sauce,orderData.SetMenu,orderData.SetDrink);
            
            orderData.Sandwiches.Add(sandwich);
            orderData.Num++;
            
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("주문을 완료하시겠어요?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "네","아니오" }),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> RequirementStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["addiorder"] = ((FoundChoice)stepContext.Result).Value;
            if ((string)stepContext.Values["addiorder"] != "네")
            {
                return await stepContext.ReplaceDialogAsync(nameof(OrderDialog), null, cancellationToken);
            }
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("추가 요구사항을 입력하세요.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["requirement"] = (string)stepContext.Result;

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            orderData.Requirement = (string)stepContext.Values["requirement"];
                
            orderData.OrderNum++;
            List<ReceiptItem> ItemList = new List<ReceiptItem> { new ReceiptItem(image:new CardImage(url: "https://www.subway.co.kr/images/common/logo_w.png")) };
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
                Facts = new List<Fact> { new Fact("주문번호", $"{orderData.OrderNum }") }, //order number DB 연결?
                Items = ItemList,
                Total = $"{orderData.Price}원"
            };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문 내역을 확인해주세요."), cancellationToken);
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(receiptCard.ToAttachment()),cancellationToken);
            /*
            var Sandwich = $"{orderData.Bread}\r\n{orderData.Menu}\r\n";
            //야채
            for (int i = 0; i < orderData.Vege.Count; i++)
            {
                Sandwich += $"{orderData.Vege[i]} ";
            }
            Sandwich += $"\r\n";
            //치즈
            for (int i = 0; i < orderData.Cheese.Count; i++)
            {
                Sandwich += $"{orderData.Cheese[i]} ";
            }
            Sandwich += $"\r\n";
            //소스
            for (int i = 0; i < orderData.Sauce.Count; i++)
            {
                Sandwich += $"{orderData.Sauce[i]} ";
            }
            Sandwich += $"\r\n";
            //추가토핑
            for (int i = 0; i < orderData.Topping.Count; i++)
            {
                Sandwich += $"{orderData.Topping[i]} ";
            }
            Sandwich += $"\r\n{orderData.Bread}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(Sandwich), cancellationToken);
            var msg = "";
            if (orderData.SetMenu!="단품")
            {
                orderData.SetDrink = (string)stepContext.Values["setdrink"];
                msg += $"\r\n세트음료:{orderData.SetDrink}";
            }
            msg += $"\r\n요구사항:{orderData.Requirement}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);
            */
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
