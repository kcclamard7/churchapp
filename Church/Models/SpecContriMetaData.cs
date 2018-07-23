using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Models
{
    public class SpecContriMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int SpecContrID { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date Of Tithe")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfSpecContr { get; set; }

        [Display(Name = "Amount Of Tithe")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }


        [Display(Name = "Member Information")]
        public int MemberID { get; set; }

    }

    [MetadataType(typeof(SpecContriMetaData))]
    public partial class SpecialContr
    {
    }
}
