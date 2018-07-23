using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Models
{
    public class ChurchMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChurchID { get; set; }
    }

    [MetadataType(typeof(ChurchMetaData))]
    public partial class Church
    {

    }
}