/* * * * * * * * * * * * * * * * * * * * *
AddOrderWF.cs

9/25/15

Overview: This allows an order to be written to a file. 
        
Constructor: 
        
Variables: DateTime date - either today's date, or the date the user wants the order to be under.     

Defined Methods: public void Execute - Asks the user if they want to use today's date as the order date, calls 
                 the QueryTheCustomer method, dislays the order.
                 private Order QueryTheCustomer - Asks the user for the order information, and returns the order.
                 

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using System.Configuration;

namespace FlooringProgram
{
    public class AddOrderWF
    {
        Repo repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

        DateTime date;

        OrderManager orderManager = new OrderManager();

        UserQuestions userQuestions = new UserQuestions();

        public void Execute()
        {
            
            if (userQuestions.Confirmation("Would you like to use today's date?"))
            {
                date = DateTime.Today;
            }
            else
            {
                date = userQuestions.AskDate();
            }


            Order order = QueryTheCustomer();


            DisplayOrdersWF displayOrder = new DisplayOrdersWF();
            
            var response = orderManager.AddOrder(order, date);

            if (response.Success)
            {
                displayOrder.PrintSingleOrder(order);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("An error occurred.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }            
        }

        private Order QueryTheCustomer()
        {
            string lastName = userQuestions.AskLastName();
            string stateAbbreviation = userQuestions.AskState();

            var state = repo.GetStateInfo(stateAbbreviation);

            string productType = userQuestions.AskProductType();
            var product = repo.GetProductsInfo(productType);

            decimal area = userQuestions.AskArea();

            decimal materialCostTotal = CalculationsManager.CalculateMaterialCost(product.CostPerSquareFoot, area);
            decimal laborCostTotal = CalculationsManager.CalculateLaborCost(product.LaborCostPerSquareFoot, area);
            decimal taxTotal = CalculationsManager.CalculateTaxTotal(materialCostTotal,
                laborCostTotal, state.TaxRate);

            var order = new Order()
            {
                StateInfo = new State()
                {
                    StateAbbreviation = stateAbbreviation,
                    StateName = state.StateName,
                    TaxRate = state.TaxRate
                },

                ProductInfo = new Product()
                {
                    ProductType = productType,
                    CostPerSquareFoot = product.CostPerSquareFoot,
                    LaborCostPerSquareFoot = product.LaborCostPerSquareFoot
                },

                LastName = lastName,

                Area = area,

                MaterialCostTotal = materialCostTotal,
                LaborCostTotal = laborCostTotal,
                TaxTotal = taxTotal,
                Total = CalculationsManager.CalculateTotal(materialCostTotal, laborCostTotal, taxTotal)
            };

            return order;
        }      
    }
}
