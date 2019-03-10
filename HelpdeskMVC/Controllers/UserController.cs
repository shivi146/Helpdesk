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
       
        readonly UserComplaintComponent userComplaintComponent;
        public UserController( UserComplaintComponent usrComplaintComponent)
        {
            
            this.userComplaintComponent = usrComplaintComponent;
        }
        [HttpGet]
        public JsonResult GetUserComplaintDetails( string  emailid,string status)
        {
            //emailId = Request.QueryString[""]
            
            return Json(userComplaintComponent.GetUserComplaintDetails(emailid, status), JsonRequestBehavior.AllowGet);
        }
    }
}