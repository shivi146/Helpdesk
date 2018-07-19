using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Models
{
    [Table("DistrictMaster")]
    public class District
    {
        public int Id { get; set; }
        public string DistrictNames { get; set; }
    }
}