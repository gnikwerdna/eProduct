using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class WizardModels
    {

        public class Step1
        {
            [Required]
            [DisplayName("Step1 Field1")]
            public string Step1Field1 { get; set; }

            [Required]
            [DisplayName("Step1 Field2")]
            public string Step1Field2 { get; set; }
        }
        public class Step2
        {
            [Required]
            [DisplayName("Step2 Field1")]
            public string Step2Field1 { get; set; }

            [Required]
            [DisplayName("Step2 Field2")]
            public string Step2Field2 { get; set; }
        }

        public class Step3
        {
            [Required]
            [DisplayName("Step3 Field1")]
            public string Step3Field1 { get; set; }

            [Required]
            [DisplayName("Step3 Field2")]
            public string Step3Field2 { get; set; }
        }


    }
}