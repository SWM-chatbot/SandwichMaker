using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class Topping
    {
        public static List<string> vege = new List<string> { "토마토", "올리브", "양파", "양상추", "파프리카", "오이", "피망", "피클", "할라피뇨" };
        public static List<string> cheese = new List<string> { "아메리칸 치즈", "슈레드 치즈", "모차렐라 치즈" };
        public static List<string> topping = new List<string> { "미트 추가", "베이컨 비츠", "쉬림프 더블업", "에그마요", "오믈렛", "아보카도", "베이컨", "페퍼로니", "치즈 추가" };
        public static List<string> sauce = new List<string> { "유자 폰즈", "랜치드레싱", "마요네즈", "스위트 어니언", "허니 머스타드", "스위트 칠리", "핫 칠리", "사우스 웨스트", "머스타드", "홀스래디쉬", "올리브 오일", "레드와인식초", "소금", "후추", "스모크 바비큐" };

        

        //메뉴
        public static Dictionary<string,int> menu_price = new Dictionary<string, int> { { "쉬림프", 5500 }, { "로티세리 바비큐 치킨", 5900 },{ "폴드포크", 5900 }, { "에그마요", 4300 }, { "이탈리안 비엠티", 5100 }, { "비엘티", 5100 }, { "미트볼", 5100 }, { "햄", 4700 },{ "치킨 데리야끼", 5600 }, { "참치", 4800 },{ "로스트 치킨", 5900 }, { "써브웨이 클럽", 5600 }, { "터키", 5100 },{ "베지", 3900 },{ "스테이크 & 치즈", 6400 }, { "스파이시 이탈리안", 5600 } };
        //추가토핑
        public static Dictionary<string,int> topping_price = new Dictionary<string, int> { { "미트 추가", 1800 }, { "베이컨 비츠", 900 }, { "쉬림프 더블업", 1800 }, { "에그마요", 1600 }, { "오믈렛", 1200 }, { "아보카도", 1300 }, { "베이컨", 900 }, { "페퍼로니", 900 }, { "치즈 추가", 900 } };
        
        
    }
}
