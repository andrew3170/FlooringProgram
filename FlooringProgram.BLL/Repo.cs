/* * * * * * * * * * * * * * * * * * * * *
Repo.cs

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

namespace FlooringProgram.Models
{
    public class Repo
    {
        private IProductStateRepo _productStateRepo;

        public Repo(IProductStateRepo dependency)
        {
            _productStateRepo = dependency;
        }

        public List<Product> LoadProducts()
        {
            return _productStateRepo.LoadProducts();
        }

        public List<State> LoadStates()
        {
            return _productStateRepo.LoadStates();
        }

        public State GetStateInfo(string stateAbbreviation)
        {
            var states = LoadStates();
            var state = new State();

            foreach (var s in states)
            {
                if (s.StateAbbreviation == stateAbbreviation)
                {
                    state.StateAbbreviation = s.StateAbbreviation;
                    state.StateName = s.StateName;
                    state.TaxRate = s.TaxRate;
                }
            }

            return state;

        }

        public Product GetProductsInfo(string productType)
        {
            var products = LoadProducts();
            var product = new Product();

            foreach (var p in products)
            {
                if (p.ProductType == productType)
                {
                    product.ProductType = p.ProductType;
                    product.CostPerSquareFoot = p.CostPerSquareFoot;
                    product.LaborCostPerSquareFoot = p.LaborCostPerSquareFoot;
                }
            }

            return product;
        }
    }
}
