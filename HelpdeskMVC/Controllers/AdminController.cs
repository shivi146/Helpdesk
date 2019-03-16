using HelpdeskMVC.Component;
using HelpdeskMVC.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Controllers
{
    public class AdminController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        readonly ApplContext Dbcontext;
        readonly UserComponent usrComponent;
        public AdminController(ApplContext context, UserComponent usrComponent)
        {
            this.Dbcontext = context;
            this.usrComponent = usrComponent;
        }

        [HttpGet]
        public ActionResult GetAllComplaints()
        {
            return View();
        }
    }
}