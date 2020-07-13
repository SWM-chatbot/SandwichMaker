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
        public static List<string> topping = new List<string> { "미트 추가", "베이컨 비츠", "쉬림프 더블업", "에그마요", "오믈렛", "아보카도", "베이컨", "페퍼로니" };
        public static List<string> sauce = new List<string> { "유자 폰즈", "랜치드레싱", "마요네즈", "스위트 어니언", "허니 머스타드", "스위트 칠리", "핫 칠리", "사우스 웨스트", "머스타드", "홀스래디쉬", "올리브 오일", "레드와인식초", "소금", "후추", "스모크 바비큐" };

        /* 구조체 형식 테스트 
        public readonly struct addi_topp
        {
            public addi_topp(string id, int price)
            {
                ID = id;
                Price = price;
            }
            public string ID { get; }
            public int Price { get; }
        }

        List<addi_topp> ex = new List<addi_topp> { new addi_topp("미트추가", 900) };
        */
        
    }
}
