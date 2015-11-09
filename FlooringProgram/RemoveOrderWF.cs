/* * * * * * * * * * * * * * * * * * * * *
RemoveOrderWF.cs

9/25/15

Overview: This allows an order or order file to be removed.
             
Variables: var date - The date of the file the user wants to remove.  
           var orderNumer - The order number of an order. 
           var order - The order to be removed.           
           bool wishToRemove - If the user wants to remove the date, true or false.

Defined Methods: public void Execute - Asks the user the date of the file they want to remove from, displays the order,
                 then conrfirms that they want to remove the order.
    

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;

namespace FlooringProgram
{
    public class RemoveOrderWF
    {
        public void Execute()
        {
            var userQuestions = new UserQuestions();
            var orderManager = new OrderManager();
            var displayWF = new DisplayOrdersWF();

            var date = userQuestions.GetDateIfValidFile();

            var orderNumber = userQuestions.GetOrderNumberIfExists(date);

            var order = orderManager.LoadOrder(orderNumber, date);

            Console.WriteLine("This is the order you wish to remove:");
            Console.WriteLine("-----------------------------------");
            displayWF.PrintSingleOrder(order);

            bool wishToRemove = userQuestions.Confirmation("\nAre you sure you wish to delete this order?");

            if (wishToRemove)
            {
                orderManager.RemoveOrder(orderNumber, date);
                Console.WriteLine("You successfully removed the order.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("The order was not removed.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
