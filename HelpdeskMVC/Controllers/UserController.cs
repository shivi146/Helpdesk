using System;
using System.Collections.Generic;
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
            var repo = new UserRepository();
            var districtList = repo.getDistrictData();
            return View(districtList);
        }

        [HttpPost]
        [ActionName("NewUserRegistration")]
        public async Task<ActionResult> NewUserRegistration(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var repo = new UserRepository();
                UpdateModel(user);
                user.District = repo.getDistrictData();
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
            return View(user);
        }

        public JsonResult IsEmailIdAvailable(string EmailId)
        {
            return Json(!dbContext.Users.Any(x => x.EmailId == EmailId), JsonRequestBehavior.AllowGet);
        }
    }
}