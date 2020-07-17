using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
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
                // 조합 저장
                AddMySandwichStepAsync,
                MySandwichResponceStepAsync,
                //단품 추가
                AddiStepAsync,
                //9 추가 요구사항
                RequirementStepAsync,
                ConfirmStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new AddToppingDialog(userState));
            AddDialog(new OrderEndDialog(userState));
            AddDialog(new AddMySandwichDialog(userState));
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
                //orderData.Initial = false;
            }
            return await stepContext.NextAsync();
        }
        private static async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["bread"] = ((FoundChoice)stepContext.Result).Value;
            
            await stepContext.Context.SendActivityAsync(Cards.GetCard("menu"), cancellationToken);


            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("메뉴를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(Topping.menu),
               }, cancellationToken);
        }
        
        private static async Task<DialogTurnResult> BreadStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync(Cards.GetCard("bread"), cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(Topping.bread),
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
                var tipMsg = MessageFactory.Text("[입력 TIP] \r\n- 기본적으로 모든 야채가 추가되어 있습니다.\r\n- 제외할 토핑은 '-'(빼기)와 토핑이름을 입력하면 추가되어 있던 토핑이 빠집니다.\r\n- 많이 넣고 싶은 토핑은 토핑이름을 입력하면 토핑이 추가됩니다. \r\n- ','(콤마)를 이용하여 한번에 많은 토핑을 추가하거나 삭제할 수 있습니다\r\n- '토핑종류'를 입력하면 토핑 카드를 다시 보여줍니다.\r\n- '완성'을 입력하면 토핑추가가 종료됩니다.\r\n- '?','가이드','help'를 입력하면 입력 TIP이 다시 출력됩니다.");
                await stepContext.Context.SendActivityAsync(tipMsg, cancellationToken);
            }

            orderData.Menu = (string)stepContext.Values["menu"];
            orderData.Price += Topping.menu_price[orderData.Menu];
            orderData.Bread = (string)stepContext.Values["bread"];
            orderData.Vege = Topping.vege;
            orderData.Cheese = new List<string>();
            orderData.Sauce = new List<string>();
            orderData.SetMenu = "단품";
            orderData.SetDrink = "-";
            orderData.Topping = new List<string>();
            orderData.AddiOrder = new List<string>();

            return await stepContext.BeginDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        }

        private async Task<DialogTurnResult> WarmupStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            if (stepContext.Result!= null)
            {
                if (orderData.Sandwiches.Count != 0)
                {
                    return await stepContext.ReplaceDialogAsync(nameof(OrderEndDialog), null, cancellationToken);
                }
                return await stepContext.EndDialogAsync();

            }
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 데울까요?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네","아니오" }),
               }, cancellationToken);
        }

        private async Task<DialogTurnResult> SetMenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            stepContext.Values["warmup"] = ((FoundChoice)stepContext.Result).Value;
            if ((string)stepContext.Values["warmup"] == "네") orderData.Warmup = true;

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
                   Choices = ChoiceFactory.ToChoices(Topping.cookie),
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
                    Choices = ChoiceFactory.ToChoices(Topping.drink),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> AddMySandwichStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            if ((string)stepContext.Values["setmenu"] != "단품")
            {
                stepContext.Values["setdrink"] = ((FoundChoice)stepContext.Result).Value;
                orderData.SetMenu = (string)stepContext.Values["setmenu"];
                orderData.SetDrink = (string)stepContext.Values["setdrink"];
            }

            Sandwich sandwich = new Sandwich(orderData.Menu,orderData.Bread,orderData.Cheese,orderData.Warmup,orderData.Topping,orderData.Vege,orderData.Sauce,orderData.SetMenu,orderData.SetDrink);
            
            orderData.Sandwiches.Add(sandwich);
            orderData.Num++;
            
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("지금 주문한 샌드위치 조합 저장할까요?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "네","아니오" }),
                }, cancellationToken);
        }
        private async Task<DialogTurnResult> MySandwichResponceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "네")
            {
                return await stepContext.BeginDialogAsync(nameof(AddMySandwichDialog), null, cancellationToken);
            }
            else return await stepContext.NextAsync();
        }
        private async Task<DialogTurnResult> AddiStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("다른 샌드위치를 추가하시겠어요?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오" }),
               }, cancellationToken);
        }
           
        private async Task<DialogTurnResult> RequirementStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            stepContext.Values["addiorder"] = ((FoundChoice)stepContext.Result).Value;
            if ((string)stepContext.Values["addiorder"] == "네")
            {
                orderData.Initial = false;
                return await stepContext.ReplaceDialogAsync(nameof(AddMySandwichDialog), null, cancellationToken);
            }
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("추가 요구사항을 입력하세요.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            stepContext.Values["requirement"] = stepContext.Result.ToString();
            orderData.Requirement = (string)stepContext.Values["requirement"];

            return await stepContext.BeginDialogAsync(nameof(OrderEndDialog), null, cancellationToken);
        }
    }
}
