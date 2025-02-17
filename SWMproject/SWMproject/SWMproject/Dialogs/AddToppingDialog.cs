﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using SWMproject.Data;
using Azure;
using System;
using Azure.AI.TextAnalytics;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace SWMproject.Dialogs
{
    public class AddToppingDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<OrderData> _orderDataAccessor;
        
        private static AzureKeyCredential credentials= new AzureKeyCredential(Startup.TAKey);
        private static Uri endpoint = new Uri(Startup.TAEndpoint);

        static Response<KeyPhraseCollection> KeyPhraseExtraction(TextAnalyticsClient client,string input)
        {
            var response = client.ExtractKeyPhrases(input,"ko");

            // Printing key phrases
            Debug.Print("Key phrases:");
            foreach (string keyphrase in response.Value)
            {
                Debug.Print($"\t{keyphrase}");
            }

            return response;
        }

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
            bool isAdding = true;
            var orderData = await _orderDataAccessor.GetAsync(stepContext.Context, () => new OrderData(), cancellationToken);
            var topping = (string)stepContext.Result;
            if (topping.Contains("-"))
            {
                isAdding = false;
            }
            if (topping.Contains("?"))
            {
                topping += ",가이드";
            }

            var client = new TextAnalyticsClient(endpoint, credentials);
            var response = KeyPhraseExtraction(client, topping);

            foreach (var pharase in response.Value)
            {
                if (pharase == "완성")
                {
                    if (orderData.Cheese.Count == 0 || orderData.Sauce.Count == 0)
                    {
                        return await stepContext.PromptAsync(nameof(ChoicePrompt),
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text("치즈 혹은 소스가 선택되지 않았어요. 이대로 주문할까요?"),
                            Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오", "주문 취소" }),
                        }, cancellationToken);
                    }
                    else
                    {
                        return await stepContext.PromptAsync(nameof(ChoicePrompt),
                            new PromptOptions
                            {
                                Prompt = MessageFactory.Text("이대로 주문할까요?"), //confirm factory 확인해보고 코드 수정하기?
                                Choices = ChoiceFactory.ToChoices(new List<string> { "네", "아니오", "주문 취소" }),
                            }, cancellationToken);
                    }
                }

                string pharase_type = null;
                if (Topping.vege.Contains(pharase))
                    pharase_type = "야채";
                else if (Topping.cheese.Contains(pharase))
                    pharase_type = "치즈";
                else if (Topping.sauce.Contains(pharase))
                    pharase_type = "소스";
                else if (Topping.topping.Contains(pharase))
                    pharase_type = "추가토핑";
                //토핑 종류
                else if (pharase.Contains("토핑"))
                {
                    //치즈 카드
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("cheese"), cancellationToken);
                    //소스 카드 보여주기
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("sauce"), cancellationToken);
                    //추가 토핑 카드 보여주기
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("topping"), cancellationToken);
                }
                //가이드
                else if (pharase.Contains("가이드") || pharase.Contains("help"))
                {
                    await stepContext.Context.SendActivityAsync(Cards.GetCard("inputTip"), cancellationToken);
                }
                //추천 소스
                else if (pharase.Contains("추천소스"))
                {
                    var sauceRecommendMsg = $"홈페이지에서 제공하는 {orderData.Menu}의 추천 소스는 [";
                    foreach(string sauce in Topping.sauce_recommend[orderData.Menu])
                    {
                        sauceRecommendMsg += $"{sauce}, ";
                    }
                    sauceRecommendMsg += "] 입니다";
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text(sauceRecommendMsg), cancellationToken);
                }
                else
                {
                    await stepContext.Context.SendActivityAsync("없는 토핑입니다, 다시 입력해주세요!");
                }

                if (pharase_type != null)
                {
                    if (isAdding)//토핑추가
                    {
                        switch (pharase_type)
                        {
                            case "야채": orderData.Vege.Add(pharase);break;
                            case "치즈": orderData.Cheese.Add(pharase);
                                if (orderData.Cheese.Count > 1)//치즈가격
                                { 
                                    orderData.Price += Topping.topping_price["치즈 추가"];
                                    orderData.Topping.Add("치즈 추가");
                                }
                                break; 
                            case "소스": orderData.Sauce.Add(pharase); break;
                            case "추가토핑": orderData.Topping.Add(pharase); orderData.Price += Topping.topping_price[pharase]; break; //토핑 가격
                        }
                        await stepContext.Context.SendActivityAsync(pharase + " 토핑이 추가되었습니다.");
                    }
                    else //토핑삭제
                    {
                        bool flag = true;
                        switch (pharase_type)
                        {
                            case "야채":
                                if (orderData.Vege.Contains(pharase)) orderData.Vege.Remove(pharase);
                                else flag = false;
                                break;
                            case "치즈":
                                if (orderData.Cheese.Contains(pharase)) 
                                { 
                                    orderData.Cheese.Remove(pharase);
                                    if (orderData.Cheese.Count > 0)
                                    {
                                        orderData.Price -= Topping.topping_price["치즈 추가"];
                                        orderData.Topping.Remove("치즈 추가");
                                    }
                                }
                                else flag = false;
                                break;
                            case "소스":
                                if (orderData.Sauce.Contains(pharase)) orderData.Sauce.Remove(pharase);
                                else flag = false;
                                break;
                            case "추가토핑":
                                if (orderData.Topping.Contains(pharase))
                                {
                                    orderData.Topping.Remove(pharase);
                                    orderData.Price -= Topping.topping_price[pharase];
                                }
                                else flag = false;
                                break;
                        }
                        if (flag)
                        {
                            await stepContext.Context.SendActivityAsync(pharase + " 토핑이 삭제되었습니다.");
                        }
                        else await stepContext.Context.SendActivityAsync(pharase + " 토핑은 이미 삭제된 토핑입니다.");
                    }
                }
            }
            return await stepContext.ReplaceDialogAsync(nameof(AddToppingDialog),null,cancellationToken);
        }

        private static async Task<DialogTurnResult> ResponceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var responce = ((FoundChoice)stepContext.Result).Value;
            if(responce == "아니오")
                return await stepContext.ReplaceDialogAsync(nameof(AddToppingDialog), null, cancellationToken);
            else if(responce == "주문 취소")
                return await stepContext.EndDialogAsync(result: "주문 취소", cancellationToken);
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
