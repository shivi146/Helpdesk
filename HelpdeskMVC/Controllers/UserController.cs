using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HelpdeskMVC.Models;

namespace HelpdeskMVC.Controllers
{
    public class UserController : Controller
    {
        ApplContext dbContext = new ApplContext();
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
        public async Task<ActionResult> NewUserRegistration(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateModel(user);
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();                
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                
            }
            return RedirectToAction("Login");
        }

        public JsonResult IsEmailIdAvailable(string EmailId)
        {
            return Json(!dbContext.Users.Any(x => x.EmailId == EmailId), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}