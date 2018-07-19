using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelpdeskMVC.Repository;

namespace HelpdeskMVC.Models
{
    public class UserRepository
    {
        public UserDetails getDistrictData()
        {

            var drepo = new DistrictRepository();
            var user = new UserDetails()
            {
                Id = Guid.NewGuid().ToString(),
               District = drepo.GetDistricts()
            };
            return user;
        }
    }
}