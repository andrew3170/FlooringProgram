/* * * * * * * * * * * * * * * * * * * * *
MainMenuWF.cs

9/25/15

Overview: This gives the user 5 options to choose from.

Variables: var input - Gets the user's input.
           

Defined Methods: public void Execute - Calls the MainMenuWF methods by the order in which they need to occur. 
                 private void DisplayMainMenu - Prints the menu options to the console.
                 private int GetUserInput - Gets the user's input and returns that int.
                 private void ChooseMenuOption - Switch statement that takes in the user's input.

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringProgram.BLL;

namespace FlooringProgram
{
    public class MainMenuWF
    {
        public void Execute()
        {

            do
            {
                DisplayMainMenu();
                int input = GetUserInput();
                ChooseMenuOption(input);

            } while (true);


        }

        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("******** Flooring Tycoon *******  (c)SGCorp");
            Console.WriteLine("       1. Display Orders");
            Console.WriteLine("       2. Add an order");
            Console.WriteLine("       3. Edit an order");
            Console.WriteLine("       4. Remove an order");
            Console.WriteLine("       5. Quit");
        }

        private int GetUserInput()
        {
            do
            {
                var input = Console.ReadLine();
                int answer;
                if (int.TryParse(input, out answer))
                {
                    if (answer > 0 && answer < 6)
                    {
                        return answer;
                    }
                    Console.WriteLine("Please input a number, 1-5.");
                }
                Console.WriteLine("That was an invalid input, please press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                DisplayMainMenu();
            } while (true);

        }

        private void ChooseMenuOption(int input)
        {
            switch (input)
            {
                case 1:
                    var displayOrdersWF = new DisplayOrdersWF();
                    displayOrdersWF.Execute();
                    break;
                case 2:
                    var addOrderWF = new AddOrderWF();
                    addOrderWF.Execute();
                    break;
                case 3:
                    var editOrderWF = new EditOrderWF();
                    editOrderWF.Execute();
                    break;
                case 4:
                    var removeOrderWF = new RemoveOrderWF();
                    removeOrderWF.Execute();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
