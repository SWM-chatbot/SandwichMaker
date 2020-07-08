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
        public static HeroCard GetBreadCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "허니오트",
                        Subtitle = "Honey Oat 230kcal",
                        Text = "고소한 위트빵에 오트밀 가루를 묻혀 고소함과 식감이 두배로",
                    
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b01.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "허니오트") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "하티",
                        Subtitle = "Hearty Italian 210kcal",
                        Text = "부드러운 화이트빵에 옥수수가루를 묻혀 겉은 바삭하고 고소하며 속은 부드럽게",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b02.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "하티") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "위트",
                        Subtitle = "Wheat 210kcal",
                        Text = "9가지 곡물로 만든 건강하고 고소한 맛의 곡물빵",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b03.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "위트") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "파마산 오레가노",
                        Subtitle = "Parmesan Oregano 210kcal",
                        Text = "부드러운 화이트빵에 파마산 오레가노 시즈닝을 묻혀 허브향 가득",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b04.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "파마산 오레가노") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "화이트",
                        Subtitle = "White 200kcal",
                        Text = "가장 클래식한 밀빵으로 부드러운 식감이 매력 포인트",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b05.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "화이트") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "플랫브래드",
                        Subtitle = "Flat Bread 230kcal",
                        Text = "이름처럼 납작 모양에 피자도우처럼 쫀득한 맛이 일품",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_b06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "플랫브래드") },
                    };
                    break;
            }
            return heroCard;
        }

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
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c01.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "아메리칸 치즈") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "슈레드 치즈",
                        Subtitle = "Shredded Cheese 50kcal",
                        Text = "치즈 설명 웅애웅",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c02.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "슈레드 치즈") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "모차렐라 치즈",
                        Subtitle = "Mozzarella Cheese 44kcal",
                        Text = "치즈 설명 웅애웅",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c03.jpg") },
                        Buttons = new List<CardAction>{ new CardAction(ActionTypes.ImBack, "선택", value: "모차렐라 치즈")},
                    };
                    break;
            }
            return heroCard;
        }

        public static HeroCard GetSauceCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "유자 폰즈",
                        Subtitle = "Yuja Ponzu 10kcal",
                        Text = "유자 향기 가득한 폰즈 소스로 상큼 업!",

                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s22.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "유자 폰즈") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "랜치드레싱",
                        Subtitle = "Ranch 110kcal",
                        Text = "고소한 마요네즈와 레몬즙의 만남! 고소함이 두배!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s01.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "랜치드레싱") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "마요네즈",
                        Subtitle = "Mayonnaise 110kcal",
                        Text = "말이 필요없는 고소함의 대명사, 마요네즈 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s02.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "마요네즈") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "스위트 어니언",
                        Subtitle = "Sweet Onion 40kcal",
                        Text = "써브웨이만의 특제 레시피로 만든 달콤한 양파소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s07.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "스위트 어니언") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "허니 머스타드",
                        Subtitle = "Honey Mustard 30kcal",
                        Text = "겨자씨가 아낌없이 들어간 달콤한 머스타드 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s03.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "허니 머스타드") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "스위트 칠리",
                        Subtitle = "Sweet Chilli 30kcal",
                        Text = "멕시코 고추와 사과퓨레의 환상적인 조화! 기분좋은 달콤함",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s12.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "스위트 칠리") },
                    };
                    break;
                case 7:
                    heroCard = new HeroCard
                    {
                        Title = "핫 칠리",
                        Subtitle = "Hot Chilli 40kcal",
                        Text = "매운맛을 보고싶다면? 태국고추로 만든 써브웨이만의 매운맛",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s18.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "핫 칠리") },
                    };
                    break;
                case 8:
                    heroCard = new HeroCard
                    {
                        Title = "사우스 웨스트",
                        Subtitle = "Chipotle 100kcal",
                        Text = "태국 고추 핫칠리와 고소한 마요네즈가 만난 이국적인 매콤한 맛",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s09.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "사우스 웨스트") },
                    };
                    break;
                case 9:
                    heroCard = new HeroCard
                    {
                        Title = "머스타드",
                        Subtitle = "Yellow Mustard 5kcal",
                        Text = "겨자씨로 만든 오리지날 머스타드 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s11.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "머스타드") },
                    };
                    break;
                case 10:
                    heroCard = new HeroCard
                    {
                        Title = "홀스래디쉬",
                        Subtitle = "Horseradish 110kcal",
                        Text = "고소한 마요네즈와 고추냉이의 이색적인 만남! 매니아층을 사로잡은 매력No1.소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s04.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "홀스래디쉬") },
                    };
                    break;
                case 11:
                    heroCard = new HeroCard
                    {
                        Title = "올리브 오일",
                        Subtitle = "Olive Oil 45kcal",
                        Text = "담백하고 깔끔하게 즐기고 싶다면 주저하지 말고 올리브오일",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "올리브 오일") },
                    };
                    break;

                case 12:
                    heroCard = new HeroCard
                    {
                        Title = "레드와인식초",
                        Subtitle = "Red Wine Vinaigrette 40kcal",
                        Text = "레드와인을 발효시켜 풍미가 가득한 식초",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s05.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "레드와인식초") },
                    };
                    break;

                case 13:
                    heroCard = new HeroCard
                    {
                        Title = "소금",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s13.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "소금") },
                    };
                    break;
                case 14:
                    heroCard = new HeroCard
                    {
                        Title = "후추",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s14.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "후추") },
                    };
                    break;
                case 15:
                    heroCard = new HeroCard
                    {
                        Title = "스모크 바비큐",
                        Subtitle = "Smoke BBQkcal",
                        Text = "스모크 향과 달콤한 바비큐의 완벽한 조화",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s17.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "스모크 바비큐") },
                    };
                    break;
            }
            return heroCard;
        }

    }
}
