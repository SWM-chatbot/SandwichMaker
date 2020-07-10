using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using SWMproject.Data;

namespace SWMproject.Dialogs
{
    public class AddToppingDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;

        public AddToppingDialog(UserState userState) : base(nameof(AddToppingDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");
            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                AddToppingStepAsync,
                LoopStepAsync
            };
            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> AddToppingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //현재 샌드위치 상태
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            var Sandwich = $"현재 샌드위치 상태 \r\n{orderData.Bread}\r\n{orderData.Menu}\r\n";
            for (int i = 0; i < orderData.Vege.Length; i++)
            {
                Sandwich += $"{orderData.Vege[i]} ";
            }
            Sandwich += $"\r\n{orderData.Bread}";
            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text(Sandwich) };

            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }
        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == "완성")
            {
                return await stepContext.EndDialogAsync(null,cancellationToken);
            }
            else
                return await stepContext.ReplaceDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        }
    }
    
}
