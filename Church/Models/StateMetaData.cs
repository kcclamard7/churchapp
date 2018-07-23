using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Church.Models
{
    public partial class StateMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int StateID { get; set; }
        [Display(Name = "Name of State")]
        public int StateName { get; set; }
    }

    [MetadataType(typeof(StateMetadata))]
    public partial class State
    {
 
    }
}