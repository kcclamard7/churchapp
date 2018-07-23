using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Models
{
    public class ExpensesMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int ExpenseID { get; set; }

        [Display(Name ="Name of Expense")]
        public string ExpenseName { get; set; }

        [Display(Name = "Expense Category")]
        public int CategoryID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date Occurs")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Amount Spent")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public decimal Description { get; set; }

        [HiddenInput(DisplayValue =false)]
        public int ChurchID { get; set; }
    }

    [MetadataType(typeof(ExpensesMetaData))]
    public partial class Expens
    {
    }
}