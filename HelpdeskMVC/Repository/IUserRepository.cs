using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskMVC.Models;
using HelpdeskMVC.Repository;

namespace HelpdeskMVC.Repository
{
   public interface IUserRepository
    {
         bool SaveUserDetails(UserDetails user);
         UserDetails checkUserLogin(LoginModel login);
    }
}
       
