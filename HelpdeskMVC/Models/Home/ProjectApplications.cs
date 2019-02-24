using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Models.Home
{
    [Table("tblProjectApplications")]
    public class ProjectApplications
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
    }

    [Table("tblModuleMaster")]
    public class Modules
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ModuleName { get; set; }
    }
}