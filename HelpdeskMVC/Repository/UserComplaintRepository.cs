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
    public class UserComplaintRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        readonly ApplContext DbContext;
        public UserComplaintRepository(ApplContext context)
        {
            this.DbContext = context;
        }
        /// <summary>
        /// Geting Applications from the database
        /// </summary>
        /// <returns></returns>
        public List<ProjectApplications> GetApplicationName()
        {
            log.Info(">>> GetApplicationName Repository layer entered");
            List<ProjectApplications> ApplicationNames =  DbContext.Applications.ToList<ProjectApplications>();
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
            List<Modules> ModuleNames = DbContext.ModuleName.Where(x=>x.ApplicationId==ApplicationId).ToList<Modules>();
            log.Debug("### get the list of modules from database");
            return ModuleNames;
        }

        public void SaveUserComplaint(UserComplaintModel userComplaint)
        {
            log.Info(">>> User Complaint save method entered");
            DbContext.UserComplaint.Add(userComplaint);
            DbContext.SaveChanges();
            log.Info(">>>User Complaint details have been saved. ");
        }
    }
}