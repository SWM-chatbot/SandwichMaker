using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Schema;

namespace SWMproject
{
    public class Cards
    {
        public static HeroCard GetCheeseCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "아메리칸 치즈",
                        Subtitle = "American Cheese 40kcal",
                        Text = "치즈 설명 웅애웅",
                        Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "아메리칸 치즈") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "슈레드 치즈",
                        Subtitle = "Shredded Cheese 50kcal",
                        Text = "치즈 설명 웅애웅",
                        Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "슈레드 치즈") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "모차렐라 치즈",
                        Subtitle = "Mozzarella Cheese 44kcal",
                        Text = "치즈 설명 웅애웅",
                        Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "모차렐라 치즈")},
                    };
                    break;
            }
            return heroCard;
        }
    }
}
