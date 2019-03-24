using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models.Home;
using HelpdeskMVC.Repository;
using log4net;
using System.Collections.Generic;

namespace HelpdeskMVC.Component
{
    public class HelpdeskComponent
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly HelpdeskRepository helpdeskRepository;


        public HelpdeskComponent(HelpdeskRepository helpdeskRepository)
        {
            this.helpdeskRepository = helpdeskRepository;
        }
        /// <summary>
        /// Getting the Application list from repository layer
        /// </summary>
        /// <returns>List of modules</returns>
        public List<ProjectApplications> GetApplicationName()
        {
            log.Debug("### GetApplicationName Entered");
            return helpdeskRepository.GetApplicationName();
        }

        /// <summary>
        /// Getting the modules list from repository layer
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        public List<Modules> GetModuleName(int ApplicationId)
        {
            log.Debug("### GetModuleName Entered");
            return helpdeskRepository.GetModuleName(ApplicationId);
        }
    }
}