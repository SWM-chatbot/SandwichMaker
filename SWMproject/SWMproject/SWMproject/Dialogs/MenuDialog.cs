using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using SWMproject.Data;
namespace SWMproject.Dialogs
{
    public class MenuDialog : ComponentDialog
    {
        //1.샌드위치 주문하기 2.키워드 주문하기 3.추천받기?
        public MenuDialog(UserState userState) : base(nameof(MenuDialog))
        {
            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                SelectMenuStepAsync,
                BeginDialogStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new OrderDialog(userState));
            AddDialog(new KeywordOrderDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> SelectMenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
               new PromptOptions
               {
                   Prompt = MessageFactory.Text("어떤 방법으로 주문하시겠습니까?"),
                   Choices = ChoiceFactory.ToChoices(new List<string> { "샌드위치 만들어서 주문하기", "키워드 주문하기" }),
               }, cancellationToken);
        }
        private static async Task<DialogTurnResult> BeginDialogStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            switch (((FoundChoice)stepContext.Result).Value)
            {
                case "샌드위치 만들어서 주문하기": return await stepContext.BeginDialogAsync(nameof(OrderDialog), null, cancellationToken); 
                case "키워드 주문하기": return await stepContext.BeginDialogAsync(nameof(KeywordOrderDialog), null, cancellationToken);
                default: return await stepContext.EndDialogAsync();
            }
        }
    }
}
