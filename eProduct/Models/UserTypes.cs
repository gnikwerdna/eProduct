using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eP.Models
{
    public class UserTypes
    {
        [Key]
        public int ID { get; set; }
        public string TypeDesc { get; set; }
    }
}