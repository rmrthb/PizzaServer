using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaServerDemo.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int TotalCost { get; set; }
        public List<PizzaOrder> PizzaOrders { get; set; }
    }
}
