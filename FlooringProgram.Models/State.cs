/* * * * * * * * * * * * * * * * * * * * *
State.cs

9/25/15

Overview: Properties that set the state information.

Defined Methods: public object Clone() - Returns a duplicate order for editing.      

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class State : ICloneable
    {
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public decimal TaxRate { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
