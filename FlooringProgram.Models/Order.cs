/* * * * * * * * * * * * * * * * * * * * *
Order.cs

9/25/15

Overview: Properties that set the order information.  

Defined Methods: public object Clone() - Returns a duplicate order for editing.    

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class Order : ICloneable
    {
        public State StateInfo { get; set; }
        public Product ProductInfo { get; set; }

        public int OrderNumber { get; set; }
        public string LastName { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCostTotal { get; set; }
        public decimal LaborCostTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
