using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class OrderData
    {
        //2. 메뉴 선택
        public string Menu { get; set; }
        //3. 빵 선택
        public string Bread { get; set; }
        //4. 치즈 선택
        public string Cheese { get; set; }
        //5. 데우기
        public string Warmup { get; set; }

        //7. 소스 선택
        public string Sauce { get; set; }
        //8. 세트 선택
        public bool SetMenu { get; set; } = false;
    }
}
