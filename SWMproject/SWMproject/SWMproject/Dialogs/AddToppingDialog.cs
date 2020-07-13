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
using SWMproject;

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
                LoopStepAsync,
                ResponceStepAsync,
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

            var Sandwich = $"[현재 샌드위치 상태] \r\n{orderData.Bread}\r\n{orderData.Menu}\r\n";
            //야채
            Sandwich += $"{PrintTopping(orderData.Vege,1)} ";
            Sandwich += $"\r\n";
            //치즈
            Sandwich += $"{PrintTopping(orderData.Cheese,0)} ";
            Sandwich += $"\r\n";
            //소스
            Sandwich += $"{PrintTopping(orderData.Sauce,1)} ";
            Sandwich += $"\r\n";
            //추가토핑
            Sandwich += $"{PrintTopping(orderData.Topping,0)} ";
            Sandwich += $"\r\n{orderData.Bread}";
            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text(Sandwich) };

            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            var topping = (string)stepContext.Result;

            if (topping == "완성")
            {
                if (orderData.Cheese.Count == 0 || orderData.Sauce.Count == 0)
                {
                    return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text("치즈 혹은 소스가 선택되지 않았어요. 이대로 주문할까요?"),
                        Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니요" }),
                    }, cancellationToken);
                }
                else 
                {
                    return await stepContext.PromptAsync(nameof(ChoicePrompt),
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text("이대로 주문할까요?"),
                            Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니요" }),
                        }, cancellationToken);
                }
            }
            switch (topping)
            {
                //야채
                case "토마토":case "올리브": case "양파":case "양상추":case "파프리카":case "오이":case "피망":case "피클":case "할라피뇨":
                    orderData.Vege.Add(topping);
                    break;
                case "-토마토": case "-올리브": case "-양파": case "-양상추": case "-파프리카": case "-오이": case "-피망": case "-피클": case "-할라피뇨":
                    topping=topping.TrimStart('-');
                    if(orderData.Vege.Contains(topping)) orderData.Vege.Remove(topping);
                    else await stepContext.Context.SendActivityAsync("이미 삭제된 토핑입니다. 다시 입력해주세요!");
                    break;
                //치즈
                case "아메리칸 치즈": case "슈레드 치즈": case "모차렐라 치즈":
                    orderData.Cheese.Add(topping);
                    //치즈 두장 토핑추가 가격 체크
                    break;
                case "-아메리칸 치즈": case "-슈레드 치즈": case "-모차렐라 치즈":
                    topping = topping.TrimStart('-');
                    if (orderData.Cheese.Contains(topping))orderData.Cheese.Remove(topping);
                    else await stepContext.Context.SendActivityAsync("이미 삭제된 토핑입니다. 다시 입력해주세요!");
                    break;
                //추가토핑
                case "미트 추가": case "베이컨 비츠": case "쉬림프 더블업": case "에그마요": case "오믈렛": case "아보카도": case "베이컨": case "페퍼로니":
                    orderData.Topping.Add(topping);
                    //가격 체크
                    break;
                case "-미트 추가": case "-베이컨 비츠": case "-쉬림프 더블업": case "-에그마요": case "-오믈렛": case "-아보카도": case "-베이컨": case "-페퍼로니":
                    topping = topping.TrimStart('-');
                    if (orderData.Topping.Contains(topping)) orderData.Topping.Remove(topping);
                    else await stepContext.Context.SendActivityAsync("이미 삭제된 토핑입니다. 다시 입력해주세요!");
                    break;
                //소스
                case "유자 폰즈": case "랜치드레싱": case "마요네즈": case "스위트 어니언": case "허니 머스타드": case "스위트 칠리": case "핫 칠리": case "사우스 웨스트": case "머스타드": case "홀스래디쉬": case "올리브 오일": case "레드와인식초": case "소금": case "후추": case "스모크 바비큐":
                    orderData.Sauce.Add(topping);
                    break;
                case "-유자 폰즈": case "-랜치드레싱": case "-마요네즈": case "-스위트 어니언": case "-허니 머스타드": case "-스위트 칠리": case "-핫 칠리": case "-사우스 웨스트":
                case "-머스타드": case "-홀스래디쉬": case "-올리브 오일": case "-레드와인식초": case "-소금": case "-후추": case "-스모크 바비큐":
                    topping = topping.TrimStart('-');
                    if (orderData.Sauce.Contains(topping)) orderData.Sauce.Remove(topping);
                    else await stepContext.Context.SendActivityAsync("이미 삭제된 토핑입니다. 다시 입력해주세요!");
                    break;
                case "토핑종류":
                    //치즈 카드
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("cheese"), cancellationToken);
                    //소스 카드 보여주기
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("sauce"), cancellationToken);
                    //추가 토핑 카드 보여주기
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("topping"), cancellationToken);

                    break;
                case "가이드" : case "?" : case "help":
                    var tipMsg = MessageFactory.Text("[입력 TIP] \r\n- 기본적으로 모든 야채가 추가되어 있습니다.\r\n- 제외할 토핑은 '-'(빼기)와 토핑이름을 입력하면 추가되어 있던 토핑이 빠집니다.\r\n- 많이 넣고 싶은 토핑은 토핑이름을 입력하면 토핑이 추가됩니다.\r\n- '토핑종류'를 입력하면 토핑 카드를 다시 보여줍니다.\r\n- '완성'을 입력하면 토핑추가가 종료됩니다.\r\n- '?','가이드','help'를 입력하면 입력 TIP이 다시 출력됩니다.");
                    await stepContext.Context.SendActivityAsync(tipMsg, cancellationToken);
                    break;
                default:
                    await stepContext.Context.SendActivityAsync("없는 토핑입니다, 다시 입력해주세요!");
                    break;
            }
                return await stepContext.ReplaceDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        }

        private static async Task<DialogTurnResult> ResponceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var responce = ((FoundChoice)stepContext.Result).Value;
            if(responce == "아니요")
                return await stepContext.ReplaceDialogAsync(nameof(AddToppingDialog), null, cancellationToken);
            else return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        //토핑 출력 형식
        public string PrintTopping(List<string> toppingType,int ver)
        {
            var Topping = "";
            List<string> tmp = new List<string>();
            int[] count = new int[toppingType.Count];
            int idx = 0;

            //중복 체크
            for (int i = 0; i < toppingType.Count; i++)
            {
                if (!tmp.Contains(toppingType[i]))
                {
                    count[idx] = 1;
                    tmp.Add(toppingType[i]);
                    idx++;
                }
                else
                {
                    int index = tmp.FindIndex(x => x.StartsWith(toppingType[i]));
                    count[index]++;
                }
            }

            //출력
            for (int i = 0; i < toppingType.Count; i++)
            {
                
                if (count[i] == 1) Topping += $"{tmp[i]} ";
                else if (count[i] > 1)
                {
                    if (ver==1) Topping += $"+{tmp[i]} ";
                    else Topping += $"{tmp[i]} X{count[i]} ";
                }
            }
            return Topping;
        }

    }

}
