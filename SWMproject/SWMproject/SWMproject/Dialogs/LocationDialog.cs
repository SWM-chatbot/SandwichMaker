using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using SWMproject.Data;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace SWMproject.Dialogs
{
    public class LocationDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;

        public LocationDialog(UserState userState) : base(nameof(LocationDialog))
        {
            _orderDataAccessor = userState.CreateProperty<OrderData>("OrderData");

            //실행 순서
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                UserInputStepAsync,
                FindShopStepAsync,
                ConfirmStepAsync
            };
            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new MenuDialog(userState));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);

            if (orderData.Initial)
            {
                orderData.Num = 0;
                orderData.Sandwiches = new List<Sandwich>();
                orderData.Price = 0;
            }
            return await stepContext.NextAsync();
        }

        private static async Task<DialogTurnResult> UserInputStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var msg = "서브웨이를 이용할 주변 역이나 주소를 입력해주세요 (예: 이대역, 서대문구, 아현동)";
            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text(msg) };
            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }

        private static async Task<DialogTurnResult> FindShopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string Input = (string)stepContext.Result;

            string site = Startup.KakaoAPI;
            string query = string.Format("{0}?query={1}", site, Input+" 서브웨이");
            WebRequest request = WebRequest.Create(query);
            string rkey = Startup.KakaoKey;

            string header = "KakaoAK " + rkey;
            request.Headers.Add("Authorization", header);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string json = reader.ReadToEnd();

            stream.Close();

            JObject jtemp = JObject.Parse(json);
            JArray array = JArray.Parse(jtemp["documents"].ToString());

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            List<string> places = new List<string>();
            places.Add("다시 검색하기");
            if (array.Count < 1)
            {
                await stepContext.Context.SendActivityAsync("검색 결과가 없습니다.");
                return await stepContext.ReplaceDialogAsync(nameof(LocationDialog));
            }
            foreach (JObject jobj in array)
            {
                string place_name = jobj["place_name"].ToString();
                string phone = jobj["phone"].ToString();
                string address_name = jobj["address_name"].ToString();
                places.Add(place_name);

                string id = jobj["id"].ToString();
                string kakaoUrl = "https://map.kakao.com/link/to/"+id;

                var heroCard = new HeroCard
                {
                    Title = place_name,
                    Subtitle = phone,
                    Text = address_name,
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl,value: kakaoUrl,title:"카카오맵에서 확인하기"),
                        new CardAction(ActionTypes.PostBack, "여기서 주문하기", value: place_name)
                    },
                };
                reply.Attachments.Add(heroCard.ToAttachment());
            }
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("주문할 서브웨이 지점을 선택하세요"), cancellationToken);
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            return await stepContext.PromptAsync(nameof(ChoicePrompt), 
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(places),
                }, cancellationToken);
        }
        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            orderData.location = ((FoundChoice)stepContext.Result).Value;
            if(orderData.location == "다시 검색하기")
            {
                return await stepContext.ReplaceDialogAsync(nameof(LocationDialog), null, cancellationToken);
            }
            return await stepContext.BeginDialogAsync(nameof(MenuDialog), null, cancellationToken);
            //return await stepContext.EndDialogAsync();
        }

    }
}
