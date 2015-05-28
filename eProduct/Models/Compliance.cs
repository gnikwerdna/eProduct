using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProduct.Models
{
    public class Compliance
    {
        //holds the data items for flowcharts
        public int ComplianceID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int subID { get; set; }
        public int level { get; set; }
        public int grp { get; set; }
        public int order { get; set; }
        public virtual Compliance compliance { get; set; }
        public int? ComplianceFormId { get; set; }
        
        
        public virtual ComplianceForm ComplianceForm { get; set; }
        
        public virtual ICollection<Product> Product { get; set; }
    }
}