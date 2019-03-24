using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using HelpdeskMVC.Repository;
using log4net;
using MVCApplWithSql.Common;
using PasswordSecurity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace HelpdeskMVC.Component
{
    public class UserComponent
    {
        /// <summary>
        /// Generate a random number.
        /// </summary>
        Random rand = new Random();
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly UserRepository userComplaintRepository;


        public UserComponent(UserRepository usrComplaintRepository)
        {
            this.userComplaintRepository = usrComplaintRepository;
        }
      
        public void SaveUserComplaint(UserComplaintModel userComplaint)
        {
            try
            {
                string Email = HttpContext.Current.Session["Email"].ToString();
                UserDetails userdetails = userComplaintRepository.GetUserDetailsByEmail(Email);
                //will generate a number 0 to 9999
                int num = rand.Next(10000);
                string complaintNo = userdetails.DistrictId + DateTime.Now.ToString("ddMMyyyy") + num;
                userComplaint.ComplaintNo = complaintNo;
                userComplaint.UserId = userdetails.Id;
                userComplaint.ComplaintDate = DateTime.Now;
                userComplaint.Status = "Open";
                userComplaint.DistrictId = userdetails.DistrictId;
                userComplaintRepository.SaveUserComplaint(userComplaint);
            }
            catch(Exception ex)
            {
                log.Error("Db Exception Occurred"+ ex.Message);
                throw new HelpdeskException("Some eror occured");
            }
        }

        public List<UserComplaintModel> GetUserComplaintDetails(string emailId,string status)
        {
            try
            {
                UserDetails userdetails = userComplaintRepository.GetUserDetailsByEmail(emailId);
                return userComplaintRepository.GetUserComplaintDetails(userdetails.Id, status);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}