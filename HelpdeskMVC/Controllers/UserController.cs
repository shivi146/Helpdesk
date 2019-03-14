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
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        UserComplaintModel userComplaint = new UserComplaintModel();
       
        readonly UserComponent userComplaintComponent;
        public UserController( UserComponent usrComplaintComponent)
        {
            
            this.userComplaintComponent = usrComplaintComponent;
        }

        [HttpGet]
        public ActionResult GetUserComplaint()
        {
            return View();
        }

        [HttpGet]
        public JsonResult abc( string  emailid,string status)
        {
            //emailId = Request.QueryString[""]
            log.Info("Email ID received in GetUserComplaintDetails " + emailid);
            log.Info("Status received in GetUserComplaintDetails" + status);
            return Json(userComplaintComponent.GetUserComplaintDetails(emailid, status), JsonRequestBehavior.AllowGet);
        }
    }
}