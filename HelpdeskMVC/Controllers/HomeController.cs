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
    public class HomeController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        readonly ApplContext Dbcontext;
        readonly UserComponent usrComponent;
        public HomeController(ApplContext context, UserComponent usrComponent)
        {
            this.Dbcontext = context;
            this.usrComponent = usrComponent;
        }
               
        UserComplaintModel userComplaintModel = new UserComplaintModel();
        
        //public ActionResult CustomError()
        //{
        //    ViewBag.Message = TempData["ApplicationError"];
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Getting the application Names
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult UserComplaint()
        {
            userComplaintModel.ApplicationName = usrComponent.GetApplicationName();
            return View(userComplaintModel);
        }
       
        /// <summary>
        /// Save The user Cmplaint
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public JsonResult UserComplaint(UserComplaintModel userComplaint)
        {
            if (ModelState.IsValid)
            {
                usrComponent.SaveUserComplaint(userComplaint);
            }
            return Json("gfdgdg");
        }

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