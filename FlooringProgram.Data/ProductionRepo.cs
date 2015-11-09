/* * * * * * * * * * * * * * * * * * * * *
ProductionRepo.cs

9/25/15

Overview: 
        
Constructor: 
        
Variables:        

Defined Methods:     

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;


namespace FlooringProgram.Data
{
    public class ProductionRepo : IProductStateRepo
    {
        private List<State> states;
        private List<Product> products;
        private string FilePathState = @"DataFiles\Taxes.txt";
        private string FilePathProduct = @"DataFiles\Products.txt";

        public List<State> LoadStates()
        {
            states = new List<State>();

            var reader = File.ReadAllLines(FilePathState);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');

                var state = new State();

                state.StateAbbreviation = columns[0];
                state.StateName = columns[1];
                state.TaxRate = Decimal.Parse(columns[2]);

                states.Add(state);
            }

            return states;
        }

        public List<Product> LoadProducts()
        {
            products = new List<Product>();

            var reader = File.ReadAllLines(FilePathProduct);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');

                var product = new Product();

                product.ProductType = columns[0];
                product.CostPerSquareFoot = Decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = Decimal.Parse(columns[2]);

                products.Add(product);

            }

            return products;
        }
    }
}
