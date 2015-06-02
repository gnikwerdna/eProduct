using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Display(Name = "Item Description")]
        public string ProductName { get; set; }
        [Display(Name = "Product Group")]
        public string ProductGroup { get; set; }
        [Display(Name = "Pronto Part No")]
        public string ProntoPartNumber { get; set; }
        [Display(Name = "Product Manager")]
        public int ProductManager { get; set; }
        [Display(Name = "Product Compliance Specialist")]
        public int ProductComplianceSpecialist { get; set; }
        [Display(Name = "Standard Reference Number")]
        public string StandardReferenceNumber { get; set; }
        public string Regulation { get; set; }
        [Display(Name = "Test House Name")]
        public string TestHouseName { get; set; }
        [Display(Name = "Accreditation Agency Name")]
        public string AccreditationAgencyName { get; set; }
        public string Licenses { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public int[] CItemIds { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Compliance> Compliance { get; set; }


    }
}