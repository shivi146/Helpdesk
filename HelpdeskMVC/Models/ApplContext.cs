using HelpdeskMVC.Models.Home;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Models
{
    public class ApplContext : DbContext
    {
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<ProjectApplications> Applications { get; set; }
        public DbSet<Modules> ModuleName { get; set; }
        public DbSet<UserComplaintModel> UserComplaint { get; set; }
        public object ModuleNames { get; internal set; }
    }
}