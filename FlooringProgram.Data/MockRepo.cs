/* * * * * * * * * * * * * * * * * * * * *
MockRepo.cs

9/25/15

Overview: 
        
Constructor: 
        
Variables:        

Defined Methods:     

* * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class MockRepo : IProductStateRepo
    {
        private List<State> states;
        private List<Product> products;
          
        public List<State> LoadStates()
        {
            states = new List<State>()
            {
                new State()
                {
                    StateAbbreviation = "OH",
                    StateName = "Ohio",
                    TaxRate = 7
                },

                new State()
                {
                    StateAbbreviation = "NY",
                    StateName = "New York",
                    TaxRate = 6
                },

                new State()
                {
                    StateAbbreviation = "CA",
                    StateName = "California",
                    TaxRate = 10
                },

                new State()
                {
                    StateAbbreviation = "TX",
                    StateName = "Texas",
                    TaxRate = 5.6M
                },

                new State()
                {
                    StateAbbreviation = "SD",
                    StateName = "South Dakota",
                    TaxRate = 3.5M
                },

                new State()
                {
                    StateAbbreviation = "WA",
                    StateName = "Washington",
                    TaxRate = 8.6M
                },
            };

            return states;
        }

        public List<Product> LoadProducts()
        {
            products = new List<Product>()
            {
                new Product()
                {
                    ProductType = "Glitter",
                    CostPerSquareFoot =  5.45M,
                    LaborCostPerSquareFoot = 1
                },

                new Product()
                {
                    ProductType = "Pokeballs",
                    CostPerSquareFoot =  600,
                    LaborCostPerSquareFoot = 10
                },

                new Product()
                {
                    ProductType = "Sod",
                    CostPerSquareFoot =  8.7M,
                    LaborCostPerSquareFoot = 10.3M
                },

                new Product()
                {
                    ProductType = "Pillows",
                    CostPerSquareFoot =  10.5M,
                    LaborCostPerSquareFoot = .50M
                },

                new Product()
                {
                    ProductType = "Ice",
                    CostPerSquareFoot =  .24M,
                    LaborCostPerSquareFoot = 50.45M
                },

            };

            return products;
        }
    }
}
