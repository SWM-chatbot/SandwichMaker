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
                //2 메뉴
                MenuStepAsync,
                //3 빵
                BreadStepAsync,
                //4 치즈
                CheeseStepAsync,
                //5 데우기
                WarmupStepAsync,
                //6 야채
                VegeStepAsync,
                //7 소스
                SauseStepAsync,
                //8 세트 선택
                SetMenuStepAsync,
                //9 추가 요구사항
                RequirementStepAsync,
                //10 주문내역
                SummaryStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
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
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("메뉴를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "쉬림프", "로티세리 바비큐 치킨", "폴드포크", "에그마요","이탈리안 비엠티","비엘티","미트볼","햄","참치","로스트 치킨","터키","베지","써브웨이 클럽","스테이크 & 치즈","스파이시 이탈리안","써브웨이 멜트","치킨 데리야끼" }),
               }, cancellationToken);
        }
        
        private static async Task<DialogTurnResult> BreadStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["menu"] = ((FoundChoice)stepContext.Result).Value;

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments.Add(Cards.GetBreadCard(1).ToAttachment());
            reply.Attachments.Add(Cards.GetBreadCard(2).ToAttachment());
            reply.Attachments.Add(Cards.GetBreadCard(3).ToAttachment());
            reply.Attachments.Add(Cards.GetBreadCard(4).ToAttachment());
            reply.Attachments.Add(Cards.GetBreadCard(5).ToAttachment());
            reply.Attachments.Add(Cards.GetBreadCard(6).ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("빵을 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "허니오트", "하티", "파마산 오레가노", "화이트","플랫브래드","위트" }),
               }, cancellationToken);
        }

        private static async Task<DialogTurnResult> CheeseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["bread"] = ((FoundChoice)stepContext.Result).Value;

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments.Add(Cards.GetCheeseCard(1).ToAttachment());
            reply.Attachments.Add(Cards.GetCheeseCard(2).ToAttachment());
            reply.Attachments.Add(Cards.GetCheeseCard(3).ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("치즈를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "아메리칸 치즈","슈레드 치즈","모차렐라 치즈" }),
               }, cancellationToken);
            
        }

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
        private static async Task<DialogTurnResult> SauseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["vege"] = ((FoundChoice)stepContext.Result).Value;

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments.Add(Cards.GetSauceCard(1).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(2).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(3).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(4).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(5).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(6).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(7).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(8).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(9).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(10).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(11).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(12).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(13).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(14).ToAttachment());
            reply.Attachments.Add(Cards.GetSauceCard(15).ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("소스를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "선택 안함","랜치","마요네즈","스위트 어니언","허니 머스타드","스위트 칠리","핫 칠리","사우스 웨스트","머스타드","홀스래디쉬","소금","후추","스모크 바비큐" }),
               }, cancellationToken);
        }

        private static async Task<DialogTurnResult> SetMenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["sauce"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("1900원 추가 지불하고 세트로 주문하시겠습니까?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오" }),
               }, cancellationToken);
        }

        private static async Task<DialogTurnResult> RequirementStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["setmenu"] = false;
            if (((FoundChoice)stepContext.Result).Value == "네")
            {
                stepContext.Values["setmenu"] = true;
            }
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("추가 요구사항을 입력하세요.") }, cancellationToken);

        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["requirement"] = (string)stepContext.Result;

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            orderData.Menu = (string)stepContext.Values["menu"];
            orderData.Bread = (string)stepContext.Values["bread"];
            orderData.Cheese = (string)stepContext.Values["cheese"];
            orderData.Warmup = (bool)stepContext.Values["warmup"];
            orderData.Vege = (string)stepContext.Values["vege"];
            orderData.Sauce = (string)stepContext.Values["sauce"];
            orderData.SetMenu = (bool)stepContext.Values["setmenu"];
            orderData.Requirement = (string)stepContext.Values["requirement"];
            

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문 내역을 확인해주세요."), cancellationToken);
            var msg = $"메뉴:{orderData.Menu} 빵:{orderData.Bread} 치즈:{orderData.Cheese} 소스:{orderData.Sauce} 빼는야채:{orderData.Vege} ";
            if (orderData.SetMenu)
            {
                msg += "세트 ";
            }
            msg += $"요구사항:{orderData.Requirement}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
