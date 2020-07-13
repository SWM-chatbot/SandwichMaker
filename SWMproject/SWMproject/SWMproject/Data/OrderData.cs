using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class OrderData
    {
        public int price { get; set; }

        //2. 메뉴 선택
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
        //단품
        public List<string> AddiOrder { get; set; }
        //9. 요구사항
        public string Requirement { get; set; }

    }
}
