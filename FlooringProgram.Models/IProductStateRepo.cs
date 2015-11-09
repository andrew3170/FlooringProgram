/* * * * * * * * * * * * * * * * * * * * *
IProductStateRepo.cs

9/25/15

Overview: Interface that gets and sets states and products from the client lists.
 
Variables: List<State> LoadStates - Loads the states from the client list Taxes.txt.
           List<Product> LoadProducts - Loads the products from the client list Products.txt.   

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public interface IProductStateRepo
    {
        List<State> LoadStates();

        List<Product> LoadProducts();
    }
}
