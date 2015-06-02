using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }


}