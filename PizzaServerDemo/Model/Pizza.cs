using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaServerDemo.Model
{
    public class Pizza
    {
        public int PizzaID { get; set; }
        public string PizzaName { get; set; }
        public int PizzaPrice { get; set; }

        public List<PizzaOrder> PizzaOrders { get; set; }
    }
}
