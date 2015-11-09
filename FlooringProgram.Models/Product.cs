/* * * * * * * * * * * * * * * * * * * * *
Product.cs

9/25/15

Overview: Properties that set the product information.

Defined Methods: public object Clone() - Returns a duplicate order for editing.   

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class Product : ICloneable
    {
        public string ProductType { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
