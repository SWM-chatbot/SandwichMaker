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
                //2~5
                //6 야채
                //7 소스
                SauseStepAsync,
                //8 세트 선택
                SetMenuStepAsync,
                //9 추가 요구사항
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
        private static async Task<DialogTurnResult> SauseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("소스를 선택해주세요."),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "스위트칠리","랜치","어니언머시기","웅앵웅" }),
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

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["setmenu"] = ((FoundChoice)stepContext.Result).Value;

            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            orderData.Sauce = (string)stepContext.Values["sauce"];
            if ((string)stepContext.Values["setmenu"] == "네"){ orderData.SetMenu = true;}
            else { orderData.SetMenu = false; }

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문 내역을 확인해주세요."), cancellationToken);
            var msg = $"소스:{orderData.Sauce},";
            if (orderData.SetMenu)
            {
                msg += "세트";
            }
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
