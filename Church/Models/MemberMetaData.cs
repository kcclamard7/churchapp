using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Models
{
    public class MemberMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int MemberID { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Born on")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name ="Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "State")]
        public string States { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }


        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Status")]
        public string MarStat { get; set; }



        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Join on")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateJoined { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Church")]
        public int ChurchID { get; set; }


    }

    [MetadataType(typeof(MemberMetaData))]
    public partial class Member
    {
    }
}