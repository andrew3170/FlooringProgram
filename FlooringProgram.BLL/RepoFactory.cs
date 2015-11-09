/* * * * * * * * * * * * * * * * * * * * *
RepoFactory.cs

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
using FlooringProgram.Data;

namespace FlooringProgram.Models
{
    public class RepoFactory
    {
        public static Repo GetRepo(string choice)
        {
            switch (choice)
            {
                case "1":
                    return new Repo(new ProductionRepo());
                default:
                    return new Repo(new MockRepo());
            }
        }
    }
}
