using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace HelpdeskMVC.Repository
{
    public class HelpdeskRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly ApplContext dbContext;
        public HelpdeskRepository(ApplContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Geting Applications from the database
        /// </summary>
        /// <returns></returns>
        public List<ProjectApplications> GetApplicationName()
        {
            log.Info(">>> GetApplicationName Repository layer entered");
            List<ProjectApplications> ApplicationNames = dbContext.Applications.ToList<ProjectApplications>();
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
            List<Modules> ModuleNames = dbContext.ModuleName.Where(x => x.ApplicationId == ApplicationId).ToList<Modules>();
            log.Debug("### get the list of modules from database");
            return ModuleNames;
        }
    }
}