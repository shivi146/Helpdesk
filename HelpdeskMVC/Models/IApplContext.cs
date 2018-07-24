using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskMVC.Models
{
    public interface IApplContext
    {
         DbSet<UserDetails> Users { get; set; }
        DbSet<District> Districts { get; set; }

    }
}
