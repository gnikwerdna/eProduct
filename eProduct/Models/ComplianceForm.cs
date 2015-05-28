using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProduct.Models
{
    public class ComplianceForm
    {
        [Key]
        public int ComplianceFormId { get; set; }
        [Display(Name="Form Name")]
        public string FormName { get; set; }
        public string Description { get; set; }
       // public virtual Compliance compliance { get; set; }
    }
}