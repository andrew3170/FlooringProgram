/* * * * * * * * * * * * * * * * * * * * *
DisplayOrdersWF.cs

9/25/15

Overview: This allows the orders of a file to be displayed on the console.
     
Variables: var date - The date of the file the user wants to display.     

Defined Methods: public void Execute - Asks the user what date of orders they want to display, 
                 public void PrintSingleOrder - Prints a single order to the console.
                 public void PrintOrders - Prints all the orders that have the same date to the console.   

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram
{
    public class DisplayOrdersWF
    {
        public void Execute()
        {
            var orderManager = new OrderManager();
            var userQuestions = new UserQuestions();

            do
            {
                var date = userQuestions.AskDate();
                var response = orderManager.DisplayOrders(date);

                if (response.Success)
                {                  
                    PrintOrders(response.Data);
                    Console.ReadLine();
                    break;
                }

                    Console.WriteLine("A file with that date does not exist. Please try again.");
                    Console.WriteLine("Press enter to continue, or (Q) to quit.");

                    var answer = Console.ReadLine();
                    if (answer.ToUpper() == "Q")
                       break;

                    Console.Clear();

            } while (true);
        }

        public void PrintSingleOrder(Order order)
        {
            Console.WriteLine("Order Number: .........................{0}", order.OrderNumber);
            Console.WriteLine("Last Name: ............................{0}", order.LastName);
            Console.WriteLine("State: ................................{0}", order.StateInfo.StateAbbreviation);
            Console.WriteLine("Tax Rate: .............................{0}", order.StateInfo.TaxRate);
            Console.WriteLine("Product Type: .........................{0}", order.ProductInfo.ProductType);
            Console.WriteLine("Area: .................................{0}", order.Area);
            Console.WriteLine("Cost Per Square Foot: .................{0}", order.ProductInfo.CostPerSquareFoot);
            Console.WriteLine("Labor Cost Per Square Foot: ...........{0}", order.ProductInfo.LaborCostPerSquareFoot);
            Console.WriteLine("Total Material Cost: ..................{0}", order.MaterialCostTotal);
            Console.WriteLine("Total Labor Cost: .....................{0}", order.LaborCostTotal);
            Console.WriteLine("Tax Total: ............................{0}", order.TaxTotal);
            Console.WriteLine("Total: ................................{0}", order.Total);
        }

        public void PrintOrders(List<Order> orders)
        {
            Console.WriteLine("\n\n");
            foreach (var order in orders)
            {
                Console.WriteLine("Order Number: .........................{0}", order.OrderNumber);
                Console.WriteLine("Last Name: ............................{0}", order.LastName);
                Console.WriteLine("State: ................................{0}", order.StateInfo.StateAbbreviation);
                Console.WriteLine("Tax Rate: .............................{0}", order.StateInfo.TaxRate);
                Console.WriteLine("Product Type: .........................{0}", order.ProductInfo.ProductType);
                Console.WriteLine("Area: .................................{0}", order.Area);
                Console.WriteLine("Cost Per Square Foot: .................{0}", order.ProductInfo.CostPerSquareFoot);
                Console.WriteLine("Labor Cost Per Square Foot: ...........{0}", order.ProductInfo.LaborCostPerSquareFoot);
                Console.WriteLine("Total Material Cost: ..................{0}", order.MaterialCostTotal);
                Console.WriteLine("Total Labor Cost: .....................{0}", order.LaborCostTotal);
                Console.WriteLine("Tax Total: ............................{0}", order.TaxTotal);
                Console.WriteLine("Total: ................................{0}", order.Total);
                Console.WriteLine("\n\n\n");
            }
        }
    }
}
