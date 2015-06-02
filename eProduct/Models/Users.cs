using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class Users
    {
          [Key]
        public int UserID { get; set; }
        [Display(Name = "First Name")]
        public string Fname { get; set; }
          [Display(Name = "Last Name")]
        public string Lname { get; set; }
        public int UserTypeID { get; set; }


    }
}