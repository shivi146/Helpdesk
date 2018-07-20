using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Models
{
    [Table("tblUsers")]
    public class UserDetails
    {
      
        public int Id { get; set; }

        public string Prefix { get; set; }

        
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Only Characters are allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Only Characters are allowed")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Select District")]
   
        public Nullable<int> DistrictId { get; set; }
        [NotMapped]
        public List<District> DistrictCollection { get; set; }

        [RegularExpression(@"^(\d{10})$",ErrorMessage ="Please Enter valid mobile number")]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailIdAvailable", "User", ErrorMessage = "This EmailId already exists.")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Confirm Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}