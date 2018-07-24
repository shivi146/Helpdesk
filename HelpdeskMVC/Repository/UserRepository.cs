using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models;
using log4net;
using MVCApplWithSql.Common;
using PasswordSecurity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

/// <summary>
/// .....@gmail.com
/// </summary>
namespace HelpdeskMVC.Repository
{
    public class UserRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        ApplContext dbContext = new ApplContext();
        public bool SaveUserDetails(UserDetails user)
        {
            log.Debug("### SaveUserDetails");
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            log.Info(">>>> Data has been saved");
            return true;
        }

        public UserDetails checkUserLogin(LoginModel login)
        {          
            log.Debug("### checkUserLogin");
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            UserDetails userDetails = dbContext.Users.Where(x => x.EmailId == login.EmailId).FirstOrDefault();            
            return userDetails;
        }
    }
}