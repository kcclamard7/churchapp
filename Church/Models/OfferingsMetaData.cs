using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Models
{
    public class OfferingsMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int OfferingID { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date Of Offering")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfOffering { get; set; }

        [Display(Name = "Amount Of Offering")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }


        [Display(Name = "Member Information")]
        public int MemberID { get; set; }
    }

    [MetadataType(typeof(OfferingsMetaData))]
    public partial class Offering
    {

    }

}