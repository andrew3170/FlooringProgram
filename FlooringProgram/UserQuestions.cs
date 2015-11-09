/* * * * * * * * * * * * * * * * * * * * *
UserQuestions.cs

9/25/15

Overview: This holds methods of questions that will be called in multiple classes.
        
Variables: DateTime date - The date the user wants the order to be under.      
           var orderNumer - The order number of an order. 
           var input - What the user enters into the console. 
           var isValidState - Bool that determines if the state entered matches a state on the client list.
           var fileIsProdcut - Bool that determines if the product entered matches a state on the client list.
           var fileIsThere - Bool that determines if the file is there or not.
           var orderIsThere - Bool that determines if the order is there or not.

Defined Methods: public DateTime AskDate - Asks user what date they want the order to be under.
                 public int AskOrderNumber - Asks user what order number they need to look up.
                 public string AskLastName - Asks user for the customer's last name.
                 public string AskLastName(string originalLastName) - Displays the original last name and asks the user
                 for an updated last name.
                 public string AskState - Asks user what state the customer lives in.
                 public string AskState(string originalState) - Displays the original state and asks the user
                 for an updated state.
                 public string AskProductType - Asks user what product the customer wants.
                 public string AskProductType(string originalProductType)- Displays the original product and asks the user
                 for an updated product.
                 public decimal AskArea - Asks user for the customer's area they need to cover with flooring.
                 public decimal AskArea(decimal originalArea)- Displays the original area and asks the user
                 for an updated area.
                 public bool Confirmation(string question) - Asks user if they are sure, yes or no.
                 public DateTime GetDateIfValidFile - Determines if there is a file under the date in question, and if not, 
                 sends the user back to the Main Menu.
                 public int GetOrderNumberIfExists(DateTime date) - Determines if thr order in question exists, and if not, 
                 sends the user back to the Main Menu.

* * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram
{
    public class UserQuestions
    {
        public DateTime AskDate()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the order date (MM/DD/YYYY): ");
                var input = Console.ReadLine();
                DateTime date;

                if (DateTime.TryParse(input, out date))
                {
                    if (date <= DateTime.Today)
                       return date;

                    Console.WriteLine("That's a date in the future.");
                }
                Console.WriteLine("That was not a valid date. Press any key to try again.");
                Console.ReadKey();

            } while (true);
        }

        public int AskOrderNumber()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the customer's order number: ");
                var input = Console.ReadLine();
                int orderNumber;

                if (int.TryParse(input, out orderNumber))
                {
                    return orderNumber;
                }

                Console.WriteLine("That was not a valid order number. Press any key to try again.");
                Console.ReadKey();

            } while (true);
        }

        public string AskLastName()
        {
            string name;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the customer's last name: ");
                name = Console.ReadLine();

                if (name == "")
                {
                    Console.WriteLine("Please enter a valid name. Press any key to continue.");
                    Console.ReadKey();
                }
            } while (name == "");

            return name;
        }

        public string AskLastName(string originalLastName)
        {
            string name;

            do
            {
                Console.Clear();
                Console.WriteLine("Existing last name on file: {0}", originalLastName);
                Console.WriteLine("Updated last name: ");
                name = Console.ReadLine();

                if (name == "")
                {
                    return originalLastName;
                }
            } while (name == "");

            return name;
        }

        public string AskState()
        {
            var repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

            string stateInput;
            var orderManager = new OrderManager();
            var states = repo.LoadStates();
            bool isValidState = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please choose from the following States: \n");

                foreach (var s in states)
                {
                    Console.WriteLine(s.StateAbbreviation);
                }


                Console.WriteLine("Enter the customer's state of residence: ");
                stateInput = Console.ReadLine();

                foreach (var s in states)
                {
                    if (s.StateAbbreviation == stateInput.ToUpper())
                        isValidState = true;
                }

                if (!isValidState)
                {
                    Console.WriteLine("Please enter a valid state. Press any key to continue.");
                    Console.ReadKey();
                }

            } while (!isValidState);

            return stateInput.ToUpper();

        }

        public string AskState(string originalState)
        {
            var repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

            string stateInput;
            var orderManager = new OrderManager();
            var states = repo.LoadStates();
            bool isValidState = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Existing state on file: {0}", originalState);

                Console.WriteLine("Please choose from the following States: \n");

                foreach (var s in states)
                {
                    Console.WriteLine(s.StateAbbreviation);
                }

                Console.WriteLine("Updated state of residence: ");
                stateInput = Console.ReadLine();

                if (stateInput == "")
                {
                    return originalState;
                }

                foreach (var s in states)
                {
                    if (s.StateAbbreviation == stateInput.ToUpper())
                        isValidState = true;
                }

                if (!isValidState)
                {
                    Console.WriteLine("Please enter a valid state. Press any key to continue.");
                    Console.ReadKey();
                }

            } while (!isValidState);

            return stateInput.ToUpper();
        }

        public string AskProductType()
        {
            var repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

            string productInput;
            var orderManager = new OrderManager();
            var products = repo.LoadProducts();
            bool isValidProduct = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please choose from the following flooring materials: \n");

                foreach (var p in products)
                {
                    Console.WriteLine(p.ProductType);
                }
                
                Console.WriteLine("Enter the customer's product of choice: ");
                productInput = Console.ReadLine();

                foreach (var p in products)
                {
                    if (p.ProductType == productInput)
                    {
                        isValidProduct = true;
                    }
                }

                if (!isValidProduct)
                {
                    Console.WriteLine("Please enter a valid product. Press any key to continue.");
                    Console.ReadKey();
                }
            } while (!isValidProduct);

            return productInput;
        }

        public string AskProductType(string originalProductType)
        {
            var repo = RepoFactory.GetRepo(ConfigurationManager.AppSettings["Mode"]);

            string productInput;
            var orderManager = new OrderManager();
            var products = repo.LoadProducts();
            bool isValidProduct = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Existing flooring material: {0} ", originalProductType);
                Console.WriteLine("Please choose from the following flooring materials: \n");

                foreach (var p in products)
                {
                    Console.WriteLine(p.ProductType);
                }

                Console.WriteLine("Enter the customer's product of choice: ");
                productInput = Console.ReadLine();

                if (productInput == "")
                {
                    return originalProductType;
                }

                foreach (var p in products)
                {
                    if (p.ProductType == productInput)
                    {
                        isValidProduct = true;
                    }
                }

                if (!isValidProduct)
                {
                    Console.WriteLine("Please enter a valid product. Press any key to continue.");
                    Console.ReadKey();
                }
            } while (!isValidProduct);

            return productInput;
        }

        public decimal AskArea()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the customer's floor area in square feet: ");
                var input = Console.ReadLine();
                decimal area;

                if (decimal.TryParse(input, out area))
                {
                    if (area > 0)
                    {
                        return area;
                    }
                }

                Console.WriteLine("That was not a valid area. Press any key to try again.");
                Console.ReadKey();

            } while (true);
        }

        public decimal AskArea(decimal originalArea)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Existing area: {0}", originalArea);
                Console.WriteLine("Updated area in square feet: ");
                var input = Console.ReadLine();
                decimal area;

                if (input == "")
                {
                    return originalArea;
                }

                if (decimal.TryParse(input, out area))
                {
                    if (area > 0)
                    {
                        return area;
                    }
                }

                Console.WriteLine("That was not a valid area. Press any key to try again.");
                Console.ReadKey();

            } while (true);
        }

        public bool Confirmation(string question)
        {
            do
            {
                Console.WriteLine("{0} (Y/N)", question);
                var answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                    return true;
                if (answer.ToUpper() == "N")
                    return false;

                Console.WriteLine("Please enter (Y)es or (N)o. Press any key to try again.");
                Console.ReadKey();
            } while (true);

        }

        public DateTime GetDateIfValidFile()
        {
            var orderManager = new OrderManager();
            DateTime date;

            do
            {
                date = AskDate();

                var fileIsThere = orderManager.FileExists(date);

                if (fileIsThere)
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                    var answer = Confirmation("Would you like to try again?");

                    if (answer == true)
                    {
                        continue;
                    }
                    else
                    {
                        MainMenuWF mainMenu = new MainMenuWF();
                        mainMenu.Execute();
                    }
                }
            } while (true);
        }

        public int GetOrderNumberIfExists(DateTime date)
        {
            var orderManager = new OrderManager();

            do
            {
                int orderNumber = AskOrderNumber();

                var orderIsThere = orderManager.OrderExists(orderNumber, date);

                if (orderIsThere)
                {
                    return orderNumber;
                }
                else
                {
                    Console.WriteLine("Order does not exist.");
                    var answer = Confirmation("Would you like to try again?");

                    if (answer == true)
                    {
                        continue;
                    }
                    else
                    {
                        MainMenuWF mainMenu = new MainMenuWF();
                        mainMenu.Execute();
                    }
                }
            } while (true);
        }
    }
}
