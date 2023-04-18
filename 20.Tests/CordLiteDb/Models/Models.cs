using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordLiteDb.Models
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        [BsonIgnore]
        public bool Active { get; set; } = false;
    }

    public class Customer
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }
}
