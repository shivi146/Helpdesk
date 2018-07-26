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

        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Only Characters are allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Only Characters are allowed")]
        public string LastName { get; set; }

        [Display(Name = "Select District")]
        public int DistrictId { get; set; }

        [NotMapped]
        public List<District> DistrictCollection { get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please Enter valid mobile number")]
        public string Mobile { get; set; }

        [Display(Name = "Email Id")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailIdAvailable", "Account", ErrorMessage = "This EmailId already exists.")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }

        [HiddenInput]
        public string UserRole { get; set; }
    }
}