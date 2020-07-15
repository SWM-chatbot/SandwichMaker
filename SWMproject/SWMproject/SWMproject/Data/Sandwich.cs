using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class Sandwich
    {
        public string Menu { get; set; }
        //3. 빵 선택
        public string Bread { get; set; }
        //4. 치즈 선택
        public List<string> Cheese { get; set; }
        //5. 데우기
        public bool Warmup { get; set; }
        //추가토핑
        public List<string> Topping { get; set; }
        //6. 야채
        public List<string> Vege { get; set; }
        //7. 소스 선택
        public List<string> Sauce { get; set; }
        //8. 세트 선택
        public string SetMenu { get; set; }
        public string SetDrink { get; set; }


        public Sandwich(string menu,string bread,List<string> cheese, bool warmup, List<string> topping,List<string> vege,List<string> sauce ,string setMenu,string setDrink) 
        {
            this.Menu = menu; this.Bread = bread; this.Cheese = cheese; this.Warmup = warmup; this.Topping = topping; this.Vege = vege; this.Sauce = sauce; this.SetMenu = setMenu; this.SetDrink = setDrink;
        }
    }
}
