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

namespace HelpdeskMVC.Controllers
{

    public class AccountController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        ApplContext dbContext = new ApplContext();
        UserComponent uComp = new UserComponent();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewUserRegistration()
        {
            UserDetails u1 = new UserDetails();
            u1.DistrictCollection = dbContext.Districts.ToList<District>();
            return View(u1);
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
                uComp.saveUserDetails(user);
            }
            return RedirectToAction("Login");
        }
        //public async Task<ActionResult> NewUserRegistration(UserDetails user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            UpdateModel(user);
        //            //user.Password = PasswordStorage.CreateHash(user.Password);                    
        //            //dbContext.Users.Add(user);
        //            uRepo.SaveUserDetails(user);
        //            //await dbContext.SaveChangesAsync();                
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            foreach (var eve in e.EntityValidationErrors)
        //            {
        //                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                foreach (var ve in eve.ValidationErrors)
        //                {
        //                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                        ve.PropertyName, ve.ErrorMessage);
        //                }
        //            }
        //            throw;
        //        }

        //    }
        //    return RedirectToAction("Login");
        //}

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

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            UserDetails userDetail = uComp.loginUser(login);
            if (userDetail == null)
            {
                ModelState.AddModelError("EmailId", "Email ID/Password Incorrect!!");
                
            }
            else
            {
                Session["userName"] = userDetail.FirstName;
                return RedirectToAction("UserProfile", "Home");
            }
            return View(login);
        }
    }
}