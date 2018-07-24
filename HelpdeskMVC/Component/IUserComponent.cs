using HelpdeskMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskMVC.Component
{
    public interface IUserComponent
    {
         bool saveUserDetails(UserDetails user);
        UserDetails loginUser(LoginModel login);
    }
}
