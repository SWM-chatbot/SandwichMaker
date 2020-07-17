using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class Topping
    {
        public static List<string> vege = new List<string> { "토마토", "올리브", "양파", "양상추", "파프리카", "오이", "피망", "피클", "할라피뇨" };
        public static List<string> cheese = new List<string> { "아메리칸 치즈", "슈레드 치즈", "모차렐라 치즈" };
        public static List<string> topping = new List<string> { "미트 추가", "베이컨 비츠", "쉬림프 더블업", "에그마요", "오믈렛", "아보카도", "베이컨", "페퍼로니", "치즈 추가" };
        public static List<string> sauce = new List<string> { "유자 폰즈", "랜치드레싱", "마요네즈", "스위트 어니언", "허니 머스타드", "스위트 칠리", "핫 칠리", "사우스 웨스트", "머스타드", "홀스래디쉬", "올리브 오일", "레드와인식초", "소금", "후추", "스모크 바비큐" };
        public static List<string> bread = new List<string> { "허니오트", "하티", "파마산 오레가노", "화이트", "플랫브래드", "위트" };
        public static List<string> menu = new List<string> { "쉬림프", "로티세리 바비큐 치킨", "폴드포크", "에그마요", "이탈리안 비엠티", "비엘티", "미트볼", "햄", "참치", "로스트 치킨", "터키", "베지", "써브웨이 클럽", "스테이크 & 치즈", "스파이시 이탈리안", "치킨 데리야끼" };
        public static List<string> cookie = new List<string> { "더블 초코칩쿠키", "초코칩쿠키", "오트밀 레이즌쿠키", "라즈베리 치즈케익쿠키", "화이트 초코 마카다미아쿠키", "칩" };
        public static List<string> drink = new List<string> { "콜라", "사이다", "닥터 페퍼" };

        //메뉴
        public static Dictionary<string,int> menu_price = new Dictionary<string, int> { { "쉬림프", 5500 }, { "로티세리 바비큐 치킨", 5900 },{ "폴드포크", 5900 }, { "에그마요", 4300 }, { "이탈리안 비엠티", 5100 }, { "비엘티", 5100 }, { "미트볼", 5100 }, { "햄", 4700 },{ "치킨 데리야끼", 5600 }, { "참치", 4800 },{ "로스트 치킨", 5900 }, { "써브웨이 클럽", 5600 }, { "터키", 5100 },{ "베지", 3900 },{ "스테이크 & 치즈", 6400 }, { "스파이시 이탈리안", 5600 } };
        //추가토핑
        public static Dictionary<string,int> topping_price = new Dictionary<string, int> { { "미트 추가", 1800 }, { "베이컨 비츠", 900 }, { "쉬림프 더블업", 1800 }, { "에그마요", 1600 }, { "오믈렛", 1200 }, { "아보카도", 1300 }, { "베이컨", 900 }, { "페퍼로니", 900 }, { "치즈 추가", 900 } };
        //추천 소스
        public static Dictionary<string, List<string>> sauce_recommend = new Dictionary<string, List<string>> { { "쉬림프", new List<string>{"스위트 칠리","랜치드레싱","핫칠리"} }, { "로티세리 바비큐 치킨", new List<string> { "스위트 칠리", "유자 폰즈" } }, { "폴드포크", new List<string> { "스모크 바비큐" } }, { "에그마요", new List<string> { "랜치드레싱","스위트 칠리" } }, { "이탈리안 비엠티", new List<string> { "랜치드레싱", "스위트 어니언", "핫칠리" } }, { "비엘티", new List<string> { "스위트 칠리", "랜치드레싱", "사우스 웨스트" } }, { "미트볼", new List<string> {} }, { "햄", new List<string> { "허니 머스타드" } }, { "치킨 데리야끼", new List<string> { "스위트 칠리", "스모크 바비큐" } }, { "참치", new List<string> { "스위트 칠리","핫칠리" } }, { "로스트 치킨", new List<string> { "스위트 어니언" } }, { "써브웨이 클럽", new List<string> { "랜치드레싱","스위트 어니언","사우스 웨스트" } }, { "터키", new List<string> { "스위트 어니언", "랜치드레싱", "사우스 웨스트" } }, { "베지", new List<string> { "레드와인식초" } }, { "스테이크 & 치즈", new List<string> { "사우스 웨스트","스모크 바비큐" } }, { "스파이시 이탈리안", new List<string> {"랜치드레싱", "핫칠리" } } };
        
    }
}
