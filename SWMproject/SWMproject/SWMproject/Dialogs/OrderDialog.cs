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
                //2 빵
                BreadStepAsync,
                //3 메뉴
                MenuStepAsync,
                //4 토핑 카드 보여주기
                ShowToppingStepAsync,

                //8 세트 선택
                SetMenuStepAsync,
                SetMenuAddiStepAsync,
                SetMenuDrinkStepAsync,
                //단품 추가

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
        private static async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["bread"] = ((FoundChoice)stepContext.Result).Value;

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            for (int i = 1; i <= 16; i++)
                reply.Attachments.Add(Cards.GetMenuCard(i).ToAttachment());
            
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);


            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("메뉴를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "쉬림프", "로티세리 바비큐 치킨", "폴드포크", "에그마요","이탈리안 비엠티","비엘티","미트볼","햄","참치","로스트 치킨","터키","베지","써브웨이 클럽","스테이크 & 치즈","스파이시 이탈리안","치킨 데리야끼" }),
               }, cancellationToken);
        }
        
        private static async Task<DialogTurnResult> BreadStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            for(int i=1;i<=6;i++) reply.Attachments.Add(Cards.GetBreadCard(i).ToAttachment());

            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "허니오트", "하티", "파마산 오레가노", "화이트","플랫브래드","위트" }),
               }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowToppingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["menu"] = ((FoundChoice)stepContext.Result).Value;

            var attachments = new List<Attachment>();

            //치즈 카드 보여주기
            var cheeseReply = MessageFactory.Attachment(attachments);
            cheeseReply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            var cheeseMsg = MessageFactory.Text("치즈 종류입니다");
            for (int i = 1; i <= 3; i++) cheeseReply.Attachments.Add(Cards.GetCheeseCard(i).ToAttachment());
            await stepContext.Context.SendActivityAsync(cheeseMsg, cancellationToken);
            await stepContext.Context.SendActivityAsync(cheeseReply, cancellationToken);

            //소스 카드 보여주기
            var sauceReply = MessageFactory.Attachment(attachments);
            sauceReply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            var sauceMsg = MessageFactory.Text("소스 종류입니다");
            for (int i = 1; i <= 15; i++) sauceReply.Attachments.Add(Cards.GetSauceCard(i).ToAttachment());
            await stepContext.Context.SendActivityAsync(sauceMsg, cancellationToken);
            await stepContext.Context.SendActivityAsync(sauceReply, cancellationToken);

            //추가 토핑 카드 보여주기
            var toppingReply = MessageFactory.Attachment(attachments);
            toppingReply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            var toppingMsg = MessageFactory.Text("추가 토핑 종류입니다");
            for (int i = 1; i <= 9; i++) toppingReply.Attachments.Add(Cards.GetToppingCard(i).ToAttachment());
            await stepContext.Context.SendActivityAsync(toppingMsg, cancellationToken);
            await stepContext.Context.SendActivityAsync(toppingReply, cancellationToken);

            //야채 카드 보여주기

            //입력 tip 
            var tipMsg = MessageFactory.Text("입력 TIP!! \r\n- 기본적으로 모든 야채가 추가되어 있습니다.\r\n- 제외할 토핑은 '-'(빼기)와 토핑이름을 입력하면 추가되어 있던 토핑이 빠집니다.\r\n- 많이 넣고 싶은 토핑은 토핑이름을 입력하면 토핑이 추가됩니다.\r\n- '토핑종류'를 입력하면 토핑 카드를 다시 보여줍니다.\r\n- '완성'을 입력하면 토핑추가가 종료됩니다.\r\n- '?','가이드','help'를 입력하면 입력 TIP이 다시 출력됩니다.");
            await stepContext.Context.SendActivityAsync(tipMsg, cancellationToken);

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            orderData.Menu = (string)stepContext.Values["menu"];
            orderData.Bread = (string)stepContext.Values["bread"];
            orderData.Vege = new List<string> { "토마토", "올리브", "양상추", "양파", "파프리카", "오이", "피망", "피클", "할라피뇨" };

            return await stepContext.BeginDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        
        }

        /*
        private static async Task<DialogTurnResult> WarmupStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["cheese"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 데울까요?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네","아니오" }),
               }, cancellationToken);
        }

        private static async Task<DialogTurnResult> VegeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["warmup"] = false;
            if (((FoundChoice)stepContext.Result).Value == "네"){
                stepContext.Values["wramup"] = true;
            }
            
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빼고 싶은 야채를 선택해주세요. "),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "선택 안함","양상추","토마토","오이","피망","양파","피클","올리브","할라피뇨" }),
               }, cancellationToken);
        }
        */
        private static async Task<DialogTurnResult> SetMenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //stepContext.Values["sauce"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("1900원 추가 지불하고 세트로 주문하시겠습니까?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오" }),
               }, cancellationToken);
        }
        private static async Task<DialogTurnResult> SetMenuAddiStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["setmenu"] = "단품";
            if (((FoundChoice)stepContext.Result).Value == "네")
            {
                stepContext.Values["setmenu"] = "세트";
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                   Prompt = MessageFactory.Text("세트 메뉴를 골라주세요"),
                   Choices = ChoiceFactory.ToChoices(new List<string> {"미니 칩","더블 초코칩쿠키","초코칩쿠키","오트밀 레이즌쿠키","라즈베리 치즈케익쿠키","화이트 초코 마카다미아쿠키" }),
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

        private static async Task<DialogTurnResult> RequirementStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Values["setmenu"] != "단품")
            {
                stepContext.Values["setdrink"] = ((FoundChoice)stepContext.Result).Value;
            }
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("추가 요구사항을 입력하세요.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["requirement"] = (string)stepContext.Result;

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            orderData.Menu = (string)stepContext.Values["menu"];
            orderData.Bread = (string)stepContext.Values["bread"];
            //orderData.Cheese = (string)stepContext.Values["cheese"];
            //orderData.Warmup = (bool)stepContext.Values["warmup"];
            //orderData.Vege = (string)stepContext.Values["vege"];
            //orderData.Sauce = (string)stepContext.Values["sauce"];
            orderData.SetMenu = (string)stepContext.Values["setmenu"];
            orderData.Requirement = (string)stepContext.Values["requirement"];
            
            //주문 가격 체크도 필요함..!
            //receipt card형식으로 수정해야함 
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문 내역을 확인해주세요."), cancellationToken);
            var msg = $"메뉴:{orderData.Menu} 빵:{orderData.Bread} 치즈:{orderData.Cheese} 소스:{orderData.Sauce} 빼는야채:{orderData.Vege} 세트:{orderData.SetMenu} ";
            if (orderData.SetMenu!="단품")
            {
                orderData.SetDrink = (string)stepContext.Values["setdrink"];
                msg += $"세트음료:{orderData.SetDrink}";
            }
            msg += $"요구사항:{orderData.Requirement}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
