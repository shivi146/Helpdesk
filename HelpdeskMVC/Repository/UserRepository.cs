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
    public class UserRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly ApplContext dbContext;
        public UserRepository(ApplContext context)
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
        /// <summary>
        /// Geting Applications from the database
        /// </summary>
        /// <returns></returns>
        public List<ProjectApplications> GetApplicationName()
        {
            log.Info(">>> GetApplicationName Repository layer entered");
            List<ProjectApplications> ApplicationNames =  dbContext.Applications.ToList<ProjectApplications>();
            log.Debug("### get the list of applications from database");
            return ApplicationNames;
        }

        /// <summary>
        /// 
        /// getting the modules names from the database.
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        public List<Modules> GetModuleName(int ApplicationId)
        {
            log.Info(">>> GetModuleName Repository layer entered");
            List<Modules> ModuleNames = dbContext.ModuleName.Where(x=>x.ApplicationId==ApplicationId).ToList<Modules>();
            log.Debug("### get the list of modules from database");
            return ModuleNames;
        }

        public void SaveUserComplaint(UserComplaintModel userComplaint)
        {
            log.Info(">>> User Complaint save method entered");
            dbContext.UserComplaint.Add(userComplaint);
            dbContext.SaveChanges();
            log.Debug(">>>User Complaint details have been saved. ");
        }
        
        public UserDetails GetUserDetailsByEmail(string strEmail)
        {            
            log.Info(">>>Get User District by Email method entered");
            UserDetails userDetails = dbContext.Users.Where(m => m.EmailId == strEmail).FirstOrDefault();        
            log.Debug(">>>Get User District by Email exited. ");
            return userDetails;
        }

        public List<UserComplaintModel> GetUserComplaintDetails(int userId,string status)
        {
            log.Info(">>>Get User District by Email method entered");
            List<UserComplaintModel> lstUserComplaintDetails = dbContext.UserComplaint.Where(m => m.UserId == userId && m.Status==status).ToList();
            log.Debug(">>>Get User District by Email exited. ");
            return lstUserComplaintDetails;
        }
    }
}