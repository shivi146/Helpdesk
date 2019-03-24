using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Repository
{
    public class AccountRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly ApplContext dbContext;
        public AccountRepository(ApplContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// User Registration data Saved.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool SaveUserDetails(UserDetails user)
        {
            log.Debug("### SaveUserDetails");
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            log.Info(">>>> Data has been saved");
            return true;
        }

        /// <summary>
        /// Get Details from db based on EmailId
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public UserDetails CheckUserLogin(LoginModel login)
        {
            log.Debug("### checkUserLogin");
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            UserDetails userDetails = dbContext.Users.Where(x => x.EmailId == login.EmailId).FirstOrDefault();
            return userDetails;
        }
    }
}