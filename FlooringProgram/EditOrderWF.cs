/* * * * * * * * * * * * * * * * * * * * *
EditOrderWF.cs

9/25/15

Overview: This allows an order to be updated.
         
Variables: DateTime date - The date of the file the user wants to edit. 
           var orderNumer - The order number of an order.
           var order1 - The order to be updated.
           var order2 - order1 after it has been updated.    

Defined Methods: public void Execute - Asks the user the date of the order they want to update, tells them that if there are any 
                 fields they want to remain the same they can just press enter. 
                 private Order MakeChanges - Creates order2, prints order1 and order2 to the console.
                 private void ConfirmChanges - Asks the user to confirm that the changes are correct.      

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram
{
    public class EditOrderWF
    {
        OrderManager orderManager = new OrderManager();
        UserQuestions userQuestions = new UserQuestions();
        DisplayOrdersWF displayOrders = new DisplayOrdersWF();
        Repo repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

        public void Execute()
        {
            DateTime date = userQuestions.GetDateIfValidFile();

            var orderNumber = userQuestions.GetOrderNumberIfExists(date);

            var order1 = orderManager.LoadOrder(orderNumber, date);

            Console.WriteLine("If you would like to retain any original information, \njust press enter instead of inputing new data.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            Order order2 = MakeChanges(order1);

            ConfirmChanges(order2, date);

        }

        private Order MakeChanges(Order order1)
        {
            Order order2 = (Order)order1.Clone();

            order2.ProductInfo = (Product)order1.ProductInfo.Clone();
            order2.StateInfo = (State) order1.StateInfo.Clone();

            order2.LastName = userQuestions.AskLastName(order1.LastName);
            order2.StateInfo.StateAbbreviation = userQuestions.AskState(order1.StateInfo.StateAbbreviation);

            var state = repo.GetStateInfo(order2.StateInfo.StateAbbreviation);

            order2.StateInfo.StateName = state.StateName;
            order2.StateInfo.TaxRate = state.TaxRate;

            order2.ProductInfo.ProductType = userQuestions.AskProductType(order1.ProductInfo.ProductType);

            var product = repo.GetProductsInfo(order2.ProductInfo.ProductType);
            order2.ProductInfo.CostPerSquareFoot = product.CostPerSquareFoot;
            order2.ProductInfo.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;

            order2.Area = userQuestions.AskArea(order1.Area);

            order2.MaterialCostTotal = CalculationsManager.CalculateMaterialCost(product.CostPerSquareFoot, order2.Area);
            order2.LaborCostTotal = CalculationsManager.CalculateLaborCost(product.LaborCostPerSquareFoot, order2.Area);
            order2.TaxTotal = CalculationsManager.CalculateTaxTotal(order2.MaterialCostTotal,
                order2.LaborCostTotal, state.TaxRate);
            order2.Total = CalculationsManager.CalculateTotal(order2.MaterialCostTotal, order2.LaborCostTotal,
                order2.TaxTotal);

            Console.WriteLine("Here is the original order: \n");
            displayOrders.PrintSingleOrder(order1);

            Console.WriteLine("\n\nHere is the updated order: \n");
            displayOrders.PrintSingleOrder(order2);

            return order2;
        }

        private void ConfirmChanges(Order order2, DateTime date)
        {
            bool confirm = userQuestions.Confirmation("\n\nWould you like to save these changes?");

            if (confirm)
            {
                var response = orderManager.EditOrder(order2, date);
                if (response.Success)
                {
                    Console.WriteLine("Update successful. Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("An error occurred.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
    }
}
