using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Models
{
    public class ApplContext : DbContext, IApplContext
    {
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<District> Districts { get; set; }
    }
}