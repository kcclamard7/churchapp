//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Church.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expens
    {
        public int ExpenseID { get; set; }
        public string ExpenseName { get; set; }
        public int ChurchID { get; set; }
        public int CategoryID { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    
        public virtual CategoryExpens CategoryExpens { get; set; }
        public virtual Church Church { get; set; }
    }
}
