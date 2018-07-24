using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Models
{
    public class LoginModel
    {        
        [Required]
        [Display(Name ="Email Id")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}