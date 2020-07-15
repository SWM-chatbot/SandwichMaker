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
            }
            return reply;
        }
    }
}
