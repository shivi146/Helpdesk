using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HelpdeskMVC.Models;
using PasswordSecurity;
using HelpdeskMVC.Component;
using log4net;
using System.Web.Security;
using System.Security.Claims;
using System.Data.Entity;

namespace HelpdeskMVC.Controllers
{

    public class AccountController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(AccountController));
        
        readonly ApplContext dbContext;
        readonly UserComponent userComponent;
        public AccountController(UserComponent usrComponent, ApplContext context)
        {
            this.userComponent = usrComponent;
            this.dbContext = context;
        }
        UserDetails userDetail = new UserDetails();
       
        [HttpGet]
        public ActionResult NewUserRegistration()
        {
            userDetail.DistrictCollection = dbContext.Districts.ToList<District>();
            return View(userDetail);
        }

        [HttpPost]
        [ActionName("NewUserRegistration")]
        public ActionResult NewUserRegistration(UserDetails user)
        {
            log.Debug("### Inside NewUserRegistration ");
            if (ModelState.IsValid)
            {
                log.Info(">>>> Registration Method Called with--" + user.EmailId);
                UpdateModel(user);
                userComponent.SaveUserDetails(user);
            }
            return RedirectToAction("Login");
        }
       
        [HttpGet]
        public JsonResult IsEmailIdAvailable(string EmailId)
        {
            return Json(!dbContext.Users.Any(x => x.EmailId == EmailId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            log.Debug("### Login Page Loaded");
            return View();
        }
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            log.Info(">>>> Login method entered");
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            UserDetails userDetail = userComponent.LoginUser(login);
            if (userDetail == null)
            {
                ModelState.AddModelError("EmailId", "Email ID/Password Incorrect!!");               
            }
            else
            {
                Session["userName"] = userDetail.FirstName;
                Session["Email"] = userDetail.EmailId;
                Session["UserRole"] = userDetail.UserRole;
                return RedirectToAction("GetUserComplaint", "User");
            }
            return View(login);
        }
    }
}