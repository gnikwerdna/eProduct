using eP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class ProductCompliance
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public int? ProductId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Compliance> Compliance { get; set; }
    }
}