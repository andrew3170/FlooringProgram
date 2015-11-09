/* * * * * * * * * * * * * * * * * * * * *
EditReceipt.cs

9/25/15

Overview: Gets and sets the OldOrder and NewOrder.
     
Variables: public Order OldOrder - The original order, before editing.
           public Order NewOrder - The new order, after editing.   

* * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class EditReceipt
    {
        public Order OldOrder { get; set; }
        public Order NewOrder { get; set; }
    }
}
