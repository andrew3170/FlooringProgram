/* * * * * * * * * * * * * * * * * * * * *
CalculationsManager.cs

9/25/15

Overview:
        This is a static class that can be called on to make the necessary calculations for the total flooring cost. 

Constructor: 
        
Variables:        

Defined Methods:
        public static decimal CalculateMaterialCost(decimal costPerSquareFoot, decimal area)
                
        public static decimal CalculateLaborCost(decimal laborCostPerSquareFoot, decimal area)

        public static decimal CalculateTaxTotal(decimal materialCostTotal, decimal laborCostTotal, decimal taxRate)

        public static decimal CalculateTotal(decimal materialCostTotal, decimal laborCostTotal, decimal taxTotal)

* * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public static class CalculationsManager
    {
        public static decimal CalculateMaterialCost(decimal costPerSquareFoot, decimal area)
        {
            return costPerSquareFoot*area;
        }

        public static decimal CalculateLaborCost(decimal laborCostPerSquareFoot, decimal area)
        {
            return laborCostPerSquareFoot*area;
        }

        public static decimal CalculateTaxTotal(decimal materialCostTotal, decimal laborCostTotal, decimal taxRate)
        {
            return (materialCostTotal + laborCostTotal) * (taxRate/100);
        }

        public static decimal CalculateTotal(decimal materialCostTotal, decimal laborCostTotal, decimal taxTotal)
        {
            return materialCostTotal + laborCostTotal + taxTotal;
        }
    }
}
