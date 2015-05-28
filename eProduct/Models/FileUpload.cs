using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProduct.Models
{
    public class FileUpload
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}