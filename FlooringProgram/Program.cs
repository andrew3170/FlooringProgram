/* * * * * * * * * * * * * * * * * * * * *
Program.cs

9/25/15

Overview: Where the program executes. Just calls the main menu where the rest of the program is called from. 
          
* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;

namespace FlooringProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainMenu = new MainMenuWF();
            mainMenu.Execute();

            Console.ReadLine();
        }
    }
}
