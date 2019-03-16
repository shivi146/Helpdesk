using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpdeskMVC.Models.Home
{
    [Table("tblUserComplaints")]
    public class UserComplaintModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Select Application")]
        public int ApplicationId { get; set; }

        [NotMapped]
        public List<ProjectApplications> ApplicationName { get; set; }

        [Required]
        [Display(Name = "Select Module")]
        public int ModuleId { get; set; }

        [NotMapped]
        public List<Modules> ModuleNames { get; set; }

        [Required]
        [Display(Name ="Complaint Subject")]
        public string ComplaintSubject { get; set; }

        [Required]
        [Display(Name ="Complaint Description")]
        public string ComplaintDescription { get; set; }
        
        public string ComplaintNo { get; set; }

        public DateTime ComplaintDate { get; set; }

        public String Status { get; set; }

        public int UserId { get; set; }
       
        public int DistrictId { get; set; }
    }
}