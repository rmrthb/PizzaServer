using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaServerDemo.Model
{
    public class PizzaOrder
    {
        //[ForeignKey("CourseID")]
        public int PizzaID { get; set; }
        public Pizza Pizza { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
    }
}
