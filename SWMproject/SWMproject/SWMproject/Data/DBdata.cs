using Microsoft.Azure.Cosmos;
using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWMproject.Data
{
    public class DBdata :IStoreItem
    {
        public string id { get; set; }
        public Sandwich Contents { get; set; }
        public string ETag { get; set; }
        public string AccountNumber { get; set; }
    }
}
