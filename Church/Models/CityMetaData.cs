using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace Church.Models
{
    public class CityMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue =false)]
        public int CityID { get; set; }

        [Display(Name = " City Name")]
        public string  CityName { get; set; }

        [HiddenInput(DisplayValue =false)]
        public int StateID { get; set; }


    }


    [MetadataType(typeof(CityMetaData))]
    public partial class City
    {
        
    }
}