using HelpdeskMVC.Component;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Controllers
{
    public class HelpdeskController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly ApplContext Dbcontext;
        readonly UserComponent usrComponent;
        public HelpdeskController(ApplContext context, UserComponent usrComponent)
        {
            this.Dbcontext = context;
            this.usrComponent = usrComponent;
        }
               
        UserComplaintModel userComplaintModel = new UserComplaintModel();        

        /// <summary>
        /// Getting the modules list
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>

        [HttpGet]
        public JsonResult GetModuleList(int ApplicationId)
        {
            log.Info(">>> GetModules Entered");
            userComplaintModel.ModuleNames = usrComponent.GetModuleName(ApplicationId);
            return Json(userComplaintModel.ModuleNames, JsonRequestBehavior.AllowGet);
        }
    }
}