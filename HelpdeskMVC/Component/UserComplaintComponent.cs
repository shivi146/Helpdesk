using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models.Home;
using HelpdeskMVC.Repository;
using log4net;
using MVCApplWithSql.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Component
{
    public class UserComplaintComponent
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        readonly UserComplaintRepository userComplaintRepository;
        public UserComplaintComponent(UserComplaintRepository usrComplaintRepository)
        {
            this.userComplaintRepository = usrComplaintRepository;
        }
        /// <summary>
        /// Getting the Application list from repository layer
        /// </summary>
        /// <returns>List of modules</returns>
        public List<ProjectApplications> GetApplicationName()
        {
           log.Debug("### GetApplicationName Entered");
           return userComplaintRepository.GetApplicationName();          
        }

        /// <summary>
        /// Getting the modules list from repository layer
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        public List<Modules> GetModuleName(int ApplicationId)
        {
            log.Debug("### GetModuleName Entered");
            return userComplaintRepository.GetModuleName(ApplicationId);
        }

        public void SaveUserComplaint(UserComplaintModel userComplaint)
        {
            try
            {
                userComplaintRepository.SaveUserComplaint(userComplaint);
            }
            catch(Exception ex)
            {
                log.Error("Db Exception Occurred"+ ex.Message);
                throw new HelpdeskException("Some eror occured");
            }
        }
    }
}