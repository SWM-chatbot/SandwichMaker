using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace SWMproject
{
    public class Cards
    {
        public static HeroCard MenuCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "쉬림프  - 5,500원",
                        Subtitle = "Shrimp 253Kcal",
                        Text = "탱글한 식감이 그대로 살아있는 통새우가 5마리 들어가 한 입 베어 먹을 때 마다 진짜 새우의 풍미가 가득",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_pm10.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "쉬림프") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "로티세리 바비큐 치킨  - 5,900원",
                        Subtitle = "Rotisserie Barbecue Chicken 350kcal",
                        Text = "촉촉한 바비큐 치킨의 풍미가득. 손으로 찢어 더욱 부드러운 치킨의 혁명",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_fl01.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "로티세리 바비큐 치킨") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "폴드포크  - 5,900원",
                        Subtitle = "Pulled Pork 420kcal",
                        Text = "7시간 저온 훈연한 미국 정통 스타일의 리얼 바비큐 폴도프크 ",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_pm08.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "폴드포크") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "에그마요  - 4,300원",
                        Subtitle = "Egg Mayo 480kcal",
                        Text = "친환경 인증 받은 농장에서 생상된 달걀과 고소한 마요네즈가 만나 더 부드러운 스테디셀러",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "에그마요") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "이탈리안 비엠티  - 5,100원",
                        Subtitle = "Italian B.M.T 410kcal",
                        Text = "7시간 숙성된 페퍼로니,살라미 그리고 햄이 만들어내는 최상의 조화",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl01.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "이탈리안 비엠티") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "비엘티  - 5,100원",
                        Subtitle = "B.L.T. 380kcal",
                        Text = "오리지널 아메리칸 베이컨의 풍미와 바삭함 그대로~",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl02.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "비엘티") },
                    };
                    break;
                case 7:
                    heroCard = new HeroCard
                    {
                        Title = "미트볼  - 5,100원",
                        Subtitle = "Meatball 480kcal",
                        Text = "이탈리안 스타일 비프 미트볼에 써브웨이만의 풍부한 토마토 향이 살아있는 마리나라 소스를 듬뿍",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl03.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "미트볼") },
                    };
                    break;
                case 8:
                    heroCard = new HeroCard
                    {
                        Title = "햄  - 4,700원",
                        Subtitle = "HAM 290kcal",
                        Text = "기본 중에 기본! 풍부한 햄이 만들어내는 입 안 가득 넘치는 맛의 향연",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl04.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "햄") },
                    };
                    break;
                case 9:
                    heroCard = new HeroCard
                    {
                        Title = "치킨 데리야끼  - 5,600원",
                        Subtitle = "Chicken Teriyaki 370kcal",
                        Text = "담백한 치킨 스트립에 달콤짭쪼름한 써브웨이 특제 데리야끼 소스와의 환상적인 만남",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_pm07.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "치킨 데리야끼") },
                    };
                    break;
                case 10:
                    heroCard = new HeroCard
                    {
                        Title = "참치  - 4,800원",
                        Subtitle = "Tuna 480kcal",
                        Text = "남녀노소 누구나 좋아하는 담백한 참치와 고소한 마요네즈의 완벽한 조화",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_cl05.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "참치") },
                    };
                    break;
                case 11:
                    heroCard = new HeroCard
                    {
                        Title = "로스트 치킨  - 5,900원",
                        Subtitle = "Roasted Chicken 320kcal",
                        Text = "오븐에 구워 담백한 저칼로리 닭가슴살의 건강한 풍미",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_fl02.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "로스트 치킨") },
                    };
                    break;

                case 12:
                    heroCard = new HeroCard
                    {
                        Title = "써브웨이 클럽  - 5,600원",
                        Subtitle = "Subway Club 310kcal",
                        Text = "명실공히 시그니처 써브! 터키, 비프, 포크 햄의 완벽한 앙상블",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_fl04.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "써브웨이 클럽") },
                    };
                    break;

                case 13:
                    heroCard = new HeroCard
                    {
                        Title = "터키  - 5,100원",
                        Subtitle = "Turkey 280kcal",
                        Text = "280Kcal로 슬림하게 즐기는 오리지날 터키 샌드위치",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_fl05.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "터키") },
                    };
                    break;
                case 14:
                    heroCard = new HeroCard
                    {
                        Title = "베지  - 3,900원",
                        Subtitle = "Veggie Delite 230kcal",
                        Text = "갓 구운 빵과 신선한 7가지 야채로 즐기는 깔끔한 한끼",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_fl06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "베지") },
                    };
                    break;
                case 15:
                    heroCard = new HeroCard
                    {
                        Title = "스테이크 & 치즈  - 6,400원",
                        Subtitle = "Steak & Cheese 380kcal",
                        Text = "육즙이 쫙~풍부한 비프 스테이크의 풍미 가득!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_pm01.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "스테이크 & 치즈") },
                    };
                    break;
                case 16:
                    heroCard = new HeroCard
                    {
                        Title = "스파이시 이탈리안  - 5,600원",
                        Subtitle = "Spicy Italian 480kcal",
                        Text = "살라미, 페퍼로니가 입안 한가득! 쏘 핫한 이탈리아의 맛",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/sandwich_pm06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "스파이시 이탈리안") },
                    };
                    break;
            }
            return heroCard;
        }
        public static HeroCard BreadCard(int index)
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
        public static HeroCard ToppingCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "미트 추가  - 1,800원",
                        Subtitle = "Meat",
                        Text = "주 재료를 2배로 더 푸짐하게 즐기세요",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_01.jpg") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "베이컨 비츠  - 900원",
                        Subtitle = "Bacon Bits",
                        Text = "짭쪼름한 베이컨 비츠로 맛의 화룡점정을!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_09.jpg") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "쉬림프 더블업  - 1,800원",
                        Subtitle = "Shrimp Double Up",
                        Text = "새우의 탱글함과 신섬함을 2배로!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_08.jpg") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "에그마요  - 1,600원",
                        Subtitle = "Egg Mayo",
                        Text = "신선한 달걀과 고소한 마요네즈의 만남",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_02.jpg") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "오믈렛 - 1,200원",
                        Subtitle = "Omelet",
                        Text = "더 부드럽게, 더 고소하게",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_03.jpg") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "아보카도  - 1,300원",
                        Subtitle = "Avocado",
                        Text = "숲 속의 버터 아보카도로 영양 UP",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_04.jpg") },
                    };
                    break;
                case 7:
                    heroCard = new HeroCard
                    {
                        Title = "베이컨  - 900원",
                        Subtitle = "Bacon",
                        Text = "진한 풍미와 바삭한 베이컨으로 특별한 나만의 써브웨이~",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_05.jpg") },
                    };
                    break;
                case 8:
                    heroCard = new HeroCard
                    {
                        Title = "페퍼로니  - 900원",
                        Subtitle = "Pepperoni",
                        Text = "입맛 당기는 페퍼로니로 써브웨이를 더 맛있게!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_06.jpg") },
                    };
                    break;
                case 9:
                    heroCard = new HeroCard
                    {
                        Title = "치즈  - 900원",
                        Subtitle = "Cheese",
                        Text = "고소한 치즈를 2배로!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_toppping_07.jpg") },
                    };
                    break;
                
            }
            return heroCard;
        }
        public static HeroCard CheeseCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "아메리칸 치즈",
                        Subtitle = "American Cheese 40kcal",
                        Text = "짭조롬하면서 깊은 맛의 대중적인 슬라이스 치즈",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c01.jpg") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "슈레드 치즈",
                        Subtitle = "Shredded Cheese 50kcal",
                        Text = "체다치즈와 잭 치즈를 잘게 갈은 풍미 있고 부드러운 혼합 치즈",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c02.jpg") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "모차렐라 치즈",
                        Subtitle = "Mozzarella Cheese 44kcal",
                        Text = "",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_c03.jpg") },
                    };
                    break;
            }
            return heroCard;
        }

        public static HeroCard SauceCard(int index)
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
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "랜치드레싱",
                        Subtitle = "Ranch 110kcal",
                        Text = "고소한 마요네즈와 레몬즙의 만남! 고소함이 두배!",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s01.jpg") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "마요네즈",
                        Subtitle = "Mayonnaise 110kcal",
                        Text = "말이 필요없는 고소함의 대명사, 마요네즈 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s02.jpg") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "스위트 어니언",
                        Subtitle = "Sweet Onion 40kcal",
                        Text = "써브웨이만의 특제 레시피로 만든 달콤한 양파소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s07.jpg") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "허니 머스타드",
                        Subtitle = "Honey Mustard 30kcal",
                        Text = "겨자씨가 아낌없이 들어간 달콤한 머스타드 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s03.jpg") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "스위트 칠리",
                        Subtitle = "Sweet Chilli 30kcal",
                        Text = "멕시코 고추와 사과퓨레의 환상적인 조화! 기분좋은 달콤함",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s12.jpg") },
                    };
                    break;
                case 7:
                    heroCard = new HeroCard
                    {
                        Title = "핫 칠리",
                        Subtitle = "Hot Chilli 40kcal",
                        Text = "매운맛을 보고싶다면? 태국고추로 만든 써브웨이만의 매운맛",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s18.jpg") },
                    };
                    break;
                case 8:
                    heroCard = new HeroCard
                    {
                        Title = "사우스 웨스트",
                        Subtitle = "Chipotle 100kcal",
                        Text = "태국 고추 핫칠리와 고소한 마요네즈가 만난 이국적인 매콤한 맛",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s09.jpg") },
                    };
                    break;
                case 9:
                    heroCard = new HeroCard
                    {
                        Title = "머스타드",
                        Subtitle = "Yellow Mustard 5kcal",
                        Text = "겨자씨로 만든 오리지날 머스타드 소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s11.jpg") },
                    };
                    break;
                case 10:
                    heroCard = new HeroCard
                    {
                        Title = "홀스래디쉬",
                        Subtitle = "Horseradish 110kcal",
                        Text = "고소한 마요네즈와 고추냉이의 이색적인 만남! 매니아층을 사로잡은 매력No1.소스",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s04.jpg") },
                    };
                    break;
                case 11:
                    heroCard = new HeroCard
                    {
                        Title = "올리브 오일",
                        Subtitle = "Olive Oil 45kcal",
                        Text = "담백하고 깔끔하게 즐기고 싶다면 주저하지 말고 올리브오일",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s06.jpg") },
                    };
                    break;

                case 12:
                    heroCard = new HeroCard
                    {
                        Title = "레드와인식초",
                        Subtitle = "Red Wine Vinaigrette 40kcal",
                        Text = "레드와인을 발효시켜 풍미가 가득한 식초",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s05.jpg") },
                    };
                    break;

                case 13:
                    heroCard = new HeroCard
                    {
                        Title = "소금",
                        Subtitle = "Salt 0kcal",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s13.jpg") },
                    };
                    break;
                case 14:
                    heroCard = new HeroCard
                    {
                        Title = "후추",
                        Subtitle = "Pepper 0kcal",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s14.jpg") },
                    };
                    break;
                case 15:
                    heroCard = new HeroCard
                    {
                        Title = "스모크 바비큐",
                        Subtitle = "Smoke BBQ 35kcal",
                        Text = "스모크 향과 달콤한 바비큐의 완벽한 조화",
                        Images = new List<CardImage> { new CardImage("https://m.subway.co.kr/images/menu/img_recipe_s17.jpg") },
                    };
                    break;
            }
            return heroCard;
        }
        public static HeroCard CookieCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Title = "더블 초코칩쿠키",
                        Subtitle = "Double Chocolate Chip 210kcal",
                        Text = "부드러운 화이트 초콜릿과 다크 초콜릿의 절묘한 조화로 더욱 풍부한 달콤함",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_03.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "더블 초코칩쿠키") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Title = "초코칩쿠키",
                        Subtitle = "Chocolate Chip 200kcal",
                        Text = "알알이 가득한 초코의 가장 클래식한 달콤함",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_04.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "초코칩쿠키") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Title = "오트밀 레이즌쿠키",
                        Subtitle = "Oatmeal Raisin 200kcal",
                        Text = "캘리포니아 건포도와 귀리에 살짝 더해진 계피향의 환상적인 조화",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_05.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "오트밀 레이즌쿠키") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Title = "라즈베리 치즈케익쿠키",
                        Subtitle = "Raspberry Cheese Cake 200kcal",
                        Text = "부드럽고 풍부한 치즈와 새콤달콤 라즈베리의 달콤한 만남",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_06.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "라즈베리 치즈케익쿠키") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Title = "화이트 초코 마카다미아쿠키",
                        Subtitle = "White Choco Macadamia 220kcal",
                        Text = "고소함 가득한 마카다미아와 달콤한 화이트 초콜릿의 환상 궁합",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_07.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "화이트 초코 마카다미아쿠키") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Title = "칩",
                        Subtitle = " Chip",
                        Text = "바삭바삭한 칩을 추가해 써브웨이를 즐겨보세요",
                        Images = new List<CardImage> { new CardImage("https://www.subway.co.kr/images/menu/img_sides_08.jpg") },
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "선택", value: "칩") },
                    };
                    break;
            }
            return heroCard;
        }
        public static HeroCard InputTipCard(int index)
        {
            var heroCard = new HeroCard();
            switch (index)
            {
                case 1:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/JJkq0P_OGf2APvfq2Q96NGMqC0RTt9V1cojXbvwT2usBDEXuyMeE5JTRNzuYNAbE7qzzBOiGNOJNBZzkf9GvPNxaFNhVZqU8OSQXOgxD48v3ct6uh_g6bONMoiy3Mi32L1SoZfhZOD_px_q8co7MpeR4uZtoAFL3OF6f0URqASl3LZh35ZApvScbwNIwhPiIU_r8uyandOJQESTv_NU23uy0z-sWJ8rW_Wd6iTEBTFDwS7ifE-4Bl_swECvNOHAhwtduzjkcTTsfXkjm3sUj__4JxYFr09JLDkCChUbFb3YHHDV-klznffAiK1x85mtx4DGhifys9aGOHh1qG2YciTpakOLgKOB9t32JrEJKrKGt0XniGw-x_feHUHSU4jyBEeIB5OB0gbTe21Uno4UpR5UMouuArj_y3u7wGgVY6qYmkuh5m0Kv8WXdMc-fNN9HSKAeKgWwXefbFRaC_UzqGfMFGK5BqHWQ6AuZjCmdnmt4Ry2uQNzJ5ncsqBsRz7WxgldBlpCreUTPkioAIsnBKyvh4ZqLaF_qngCtmOJ8y-Gs8W7i6UPSgiX_1-aiL8x67C75T1_xXcfX0kNu6zakOK5YeIRxv2-5lsu6D-EUvY-u2Id_q84HcZsh236OsjlR1Y8jbJqqHZnGA2P3dlsK1JLZ2LA08nd3hXii2EsNZS-iri3I_DTLFH7BnJud=s768-no?authuser=0") },
                    };
                    break;
                case 2:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/2wKwYlZYudXb9uUtKF9YnASF28bJKYuzsp5DP1Vhu1ZqfHxMUccDKwtJ90sI7Hc4X4NV2s6k8G7OCi8ak1-ml1lhgzdtmlreDJHlT3KZ3-n18CPB_YO4qArfH2VqMuqoxnw3jiWGpdWKO6DUdFSYu5auifrqDIXB-CewV6dZzpLdfYrGjTDPUfQJ4mM6jmoRjAiV0VvQ-g6IPUV5aWxN6_xOPQ0G8GTv9LzNB1ZKOAUY_5KNvf8AgRRzml7UmB-I1x0Lh7eSQDGdzo5JdJ-YFu7KRtg-4bwxuNgdargTcO1HnHpOqfU6ktekGRwJms_Z_jY0gVNhsG_T_HEKW0sb-3z-Aur_JcxddDCvabW4FBDc27vufwqYehqNOL8gzlV7ai1gACAtDMHgAuLoQPyHx6BVTbXkK0klueJ3OQk6mM0cYw6q9j-h6rr1E1wroKWvPJXYMKP8Bx9zrT2dO4Tp2vut90RXa-tAh0h10kIgfmaxpWwm7pfh_vCTvxmbflAH2dHjJcSu2iNkwAEdJAXa1Bj2iKoh2NOgop26gAYhRXf7sD0GKbK-Qzat0xt2n06moeATocoafndZegTvnEBnYN2sZPQ3Of_6_5T8it9Uv8Dl_94I0VLftItgVJOycqjXWANNTtWZVdU4xG7ilLMyAUDJmJSE2EstZgqowctv_miJjnoeUkjoJUujWSth=s768-no?authuser=0") },
                    };
                    break;
                case 3:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/9jMqR9RnlfrBRz5dQD7Xv5KVoqOZATh9uWUHtxFly7i1U9NBSpSOKCdtSGJ_GTeVZAxXW_mXlFl9dfgHGTvVVXBp1bv56GUbWliOrhbSNF-5l_R5RF7x7X9XZNaDjJO3yiyLOAUtXc6zzrSKQZGFpx_o4ujnjH_uauX3sAxdcQlMYeDSCmicfsZsrDZkFePKQ6yLxNZ7ZJaAux5A4zFESvNycMo8efg7SNT8V5_gg56H9tnfDdM-oNr_2NI_OITQUu1N417FJfFdU0NCECFzldvSZ-ZN1VZklL8aRjpsPhbZJ0cpJ1PiAm1jjnFTXNgZ3Tv1x_KQvDVVNIqR4fDWv5J6qgHlYNGIheQLrH7Q_wc3JQT6735xC5rmdY1PRlpR4oKxhzWssUCWE0QmhtsAe3Fy1f8lWkgDNYzfRkIy-feKfkczjN8DyFYfdwX9fWcRoNac0kTHWdLPIW71Xoc1E62Ll0P-JubY-ITqMeUmaQUHSdmp_NByDG47PvG2qXqq5WXvIsjUvKJTiroK9rSyCTZ1347BIClEtFSdp_KuTUl6gW45AvimCl-t9ZGWZ8DMfVYKueY57L57JQJa6nMd1lx9ksdlRT5o-ON-Tf-iXtX9xsxmSJ05tjN_zPE2OIB9w1gOPzsrqcKLXEn7YkHLTTlygLVSmOcAVajQVZDNpcsQbnPdoDBPjSbZSunJ=s768-no?authuser=0") },
                    };
                    break;
                case 4:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/2aYqrDFQGNqTQvT1k986U94GYYqnhthdDp68YuEKVVQUQmEdRIBw5wjFJY4LM_NYMVsqbNxFq-TxKa7ClpNTMP6GYQaW-O4MQag3RREHEVbx0XtHwUIvbafvaA54MK1rFQLOu8imYtK4QXLSjGBe0b62yhLtI68X1mP4SH8VqQz_6O4HgiPIOMytE21UHVIxX85az_5f7ofrZEjqhR_lVIx4Ilkuto05bmjOWiqGfXSNfkiIxcZ-YLyRyVgOdKHFdT8m0sszuBklIZk4aoNKGVbkFt-E3GArYYZmqvSgDmnTgUyd19D_LnhLV08PekyyMmtrr5zAcb79QE_Zg2E6_nhcL34BtewXUJqxr9T1SqFXcOgxezh5OO3uG6NgRvlPj4XmaBD-iFud24ey6rhvTxEIbga8UZ2WPc1ts1lOQiBQoduIEPeUAzf1hHW5mFxDNMZW5LPFhvepIRDI70-87YV9JucFQ50IbkQPHDW9krA9vDjE6Dy6B9_6mmZlrnb08w8ZsHA6en8T346b9xaOickV8IOWoRVUmdH7OC3ktWiqSJcUnkkGp43QgBtx0U5isG0One0le-u9fW6YDCEsAOoh1adknKv1EGDzTP7onngGKIw1wzoHztkS-_Oe3vvogDS5FQVjvq0Q-Sy-1yQ-7CUyJvCHU1RtJmSIuTc8eIkdQA9Iqw-Mz8kEuR7u=s206-no?authuser=0") },
                    };
                    break;
                case 5:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/Y2YNSCIewDKVk796KLwAOIOhbeu2eAbmZCpL1dSKP3SdbLFjSk-aWs2_OSQCM1P1GP-yorwdKKk2EQDXkxVQcbKlCgevHJtP1EHbw0VVr-yUR7f3Y-eKNkFZAlKskRves5VJQe8bma3JuXBP-N0ujjWyvqdwQhta7pS7E_wkP5CDIYYByajkA_BzlN8HivJigizrp2E3OHy0hBogVBQc6ontwUyhbxY9x-wh20MP9XIfBbvJqa2n_LtjU2MxcaywUu8w0MHhk1I9F4IgmIfvFPUNc9umoNIA1oilR5XBxX_8uUkrKyCGcXZuSRMJ7MV9S_zA-A28Zd3efbzmY3OY7f7lrEBPrmsrhEBlJHmFa4UNdra1jgP6Iz_-bPBepNo7L7OaZIx0B9GtxRlWwF_2GJ7UvcLnboXdHKpwwX1DRvvMrVsiO4VSCcFqOK9nszvtpFIVURcetNCwXVV7LYeiB53smCT3DtYpH6BTUENZ9FXpPn9E73ZsbNXfM3BTfZba5Tm5hdi9YCqdi2-5Z8i1os24FvrgS9kPu8z7qRLSwldm2_VjcT2yCVH3L6W3K6u9RE0Y5mAAWawmhV-3Aug9snk9CJsF5agGI6x_JzOZcQeoIPprCE4OZOo4h5IYVsoyMjhxCoOnNtfFyeZnOyd8G_jHz3AsllDVGzrk1Qzwf8-Hur9u5-ct9oskaiF4=s206-no?authuser=0") },
                    };
                    break;
                case 6:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/dy18F0-uIpMu0PhgWmu1WCCWB73kXHPhnawxBVyN2phbt_83e1UE3w9lV-DicXRIPF0xr4OpelcORODSSZLM8octuclvx3XE3FHcI8cl5iZ_eIQJIHvRLgejgq7RN0lj5ecXF7CdPNe14UqDUvhlGlnrFR4lOavVrcC90DU1-kYsU2X3j7fYKwepE8wNDLOu3BhUXI9a2UhS5PGC8Q4RjjbYF5hTAmkxHewW8mM22s_QdlC90of-h66MIqjs0NaX470tAVi9aH4AppR0u7Tghwc5jSlV19F6ggNwOUrZmfF3mosMtQsRBrDUED3ERdKJfJsY0szYAcMB6zL_DYibBUhvWHDOIY4v-7Mj1jPvrE5d-xy25sbeTH4ZsgzZQtjILvwWSdbPXTZH6JraeF-UjGnnES--I0jYlKcDadl6D1bp2vVmZ1eGQy8SyZLpoUSf3Cp5HJV0uIEXni8MKiuOR1XmuCnHry5r2UA6tCwxpOINpIuiPROBN1J0H8ml8XYiugwDlCrIAkrXbJgpC4egUnYpN2GmS5x3zOJEBfmCvhJg4MpEbetFESgYHgyWxtO0LeVu7h_OZsds1V1WtOjvjpn_tjkfx_llek0Ww_59I2tlXAj0qZBMoE0a89gYCRJT6DZzllevk2frZcx-pou6pERiI2ZQNIDpDyciZlJKd9KaUX0J0IgXi116OV1J=s768-no?authuser=0") },
                    };
                    break;
                case 7:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/CvcFwP0CEH0RnHPYivtbdJPTy6ndf7am3TBOSVQYU9sqm1NuBoNEWGdoD5ETN_hRNHyIrIE19iFBkjNvu6wOfb9y6LD_XWXSnP6HB9ZG7gCP36wMFAb3OM4S4EQzmZituZu2GquVBhp3MNw3IRcd76ARgS8S0xwNZAsAHNlybz0oGaRer-f5XQQzvPnFLrfAmSE4IdZfv0XuSTnKQlkYcw7KILmDjgV6rp-ywIc4nUNgIq2M7Xgc9YieguKwmaITLw6GVZuqi7X_9vbo-80PPJ5p5tfKdYj7EDtDtW9h40wVfI7JgZzVya6lySpEgPE-pzUEu281NO1gfxLTLZVky_97AxVCmLFAOdOXhD-cY_VU9SvOhztlxv8VcX88W5RAt5OyQfrWVrMJGf9eNp-HOTRfkzGLGqo7vaSQTTJY-_bI1V47OrwyHgalKNReAq0GDVOt2LDex5gftL9i0bbvCCLW-6MgznLa2Z0tbgY8Hg3H-o-hX0OdLvAWs2iFPTX2lnd22Dj441W92CJeqd1Dkjj5AHSVHdr4OVMAq7rCM20VDWNtSSMb3XjEQFtoxwNU9U7pc_L0P_iB09eWz4ERiHCI7hDvxljJFLJBU9lgH7t3t5dDhNRDHn4h5zejZkW2c2j8FsRnMeelDKHQhGgijxBYZu7Wad3NqZRjpNSG0eHCOr7OZIx1SvFSwy4_=s768-no?authuser=0") },
                    };
                    break;
                case 8:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/2j1W6TK_qcGjcRauSH9bkTVECw-OSWbCSUsz_PcI6q3HcXvUwNw-8r7G-sLd3_jlLpYxTuctIO42rzy6Jkq6VzuW3a449s7ROmoBt6Z-hD5lxxrr2_SVobUrodTTaAsGj7GTgJeYbhd3GcbEdUMG7KlCjoZG4oc6IcU-RUkgOqX1lyse4I4ifo_fvl8vl7TgtjaQWy5w2aREXUSen-qLvufrkOgNrXZ_vzZizjBIlwT9eAton9plOKdvRKjaAOFOT0o2HMItFHL_ywKgZmwb69c9-5YUma6J1bDwrvgjTZxEfc_uBlJWCCsDbW4gMZn4kdz0G6f8l35BGMvLG66HVGbhBbSgZRGQ823FVJny6HMyelk2kBHnI7yPflAhPgjxdkizika0sLJMeR7iZjYzsKBkqsduExPDYKwBPhMDMEd5iL2OSXyOvbmuCHC7QGAKhz_G1xvKHu2Fuv0ZCibNDHzmw8xDdRnR37TjYWkjQft_ExtKelT8Xn7m65nUst7uU1_JFNU0aYhDKWX7qosxziaC-1mkOQyOT4HBO1ZXhuI4KhQzCY9b7TxLvjVVdy1QeqlBI66j0rsp4TFpE9GZ2QgxMQdkJZNxdlWPddEFpJs7fkeuAnJimERekgg69H2C1PnoV4LCOKELArdjqxBTH3ugFKb9MtHAz2C-V9YWKWTOcbIMg6t6u-k1yrB4=s768-no?authuser=0") },
                    };
                    break;
                case 9:
                    heroCard = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage("https://lh3.googleusercontent.com/YV0aFEqVSl_VpJILmmdBOw3un_YJsk_67TH4fVTU4LHODt5sXwggRO87zzfIgunCnglUxId_fkjH80KIwbSZfGEERbjXx68VOBgUGgxCaUTpgkWPjXofOy1DwwESBXMzi1nypozGgwSc3OuW-grUa91DsK5z26zZ1Adv2VP5vNPF1Jg15vq8T7LoDycEImnPkQYfJJxKDwAc2aIOIGUnsda_9tRVrK4W_Sm6BW6-Ua1stlIq9t8lcv28xW1i5nv8bleJ2bMBsJ13AqFtUBXxVA-ATCXKz1W3mC8oBeWkDarYi0Y7CT_g6zArRudDz7bhrE0rcy3LICmzmyii5tX9PvVlFnb0JYMpKo_kMeME3D7kqnS8cr9of3R80fSKPbgOpQaYNoOGHy6wKwpBrWmw62BRLvzh13FlGf4vR87VPWJrkgM9DhPDkSOPWpCh4uhFXxxUh7Drir0Eey1c6bSlcC6NjM-g4vxOYrek9zuqgzE8JwDy7dZ6jMz99b9yHRBSF8oF8xKr9RmT8EIw0p0fcAzyswKKL31xODR_TNk_Jqu5HHoODFenF2cSKIhQzIhO7-tFbt88vbIrlvrxyPX2gBD4glRlJOQtrDHzHmqm9y2LCL_cfQQk1apdh41RcL0xrV0KLwKqhRVqcV7_ogDmkSrAsocpzL6fbDDv8hXeDwo1gmOqCEFoIBiX5PB6=s768-no?authuser=0") },
                    };
                    break;
            }
            return heroCard;
        }
        public static IMessageActivity GetCard(string msg)
        {
            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            int i = 1;
            switch (msg)
            {
                case "menu":
                    for (i = 1; i <= 16; i++) reply.Attachments.Add(Cards.MenuCard(i).ToAttachment());
                    break;
                case "bread":
                    for (i = 1; i <= 6; i++) reply.Attachments.Add(Cards.BreadCard(i).ToAttachment());
                    break;
                case "topping":
                    for (i = 1; i <= 9; i++) reply.Attachments.Add(Cards.ToppingCard(i).ToAttachment());
                    break;
                case "cheese":
                    for (i = 1; i <= 3; i++) reply.Attachments.Add(Cards.CheeseCard(i).ToAttachment());
                    break;
                case "sauce":
                    for (i = 1; i <= 15; i++) reply.Attachments.Add(Cards.SauceCard(i).ToAttachment());
                    break;
                case "cookie":
                    for (i = 1; i <= 6; i++) reply.Attachments.Add(Cards.CookieCard(i).ToAttachment());
                    break;
                case "inputTip":
                    for (i = 1; i <= 9; i++) reply.Attachments.Add(Cards.InputTipCard(i).ToAttachment());
                    break;
            }
            return reply;
        }
    }
}
