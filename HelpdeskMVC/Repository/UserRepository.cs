using HelpdeskMVC.Controllers;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace HelpdeskMVC.Repository
{
    public class UserRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly ApplContext dbContext;
        public UserRepository(ApplContext context)
        {
            this.dbContext = context;
        }

        public void SaveUserComplaint(UserComplaintModel userComplaint)
        {
            log.Info(">>> User Complaint save method entered");
            dbContext.UserComplaint.Add(userComplaint);
            dbContext.SaveChanges();
            log.Debug(">>>User Complaint details have been saved. ");
        }
        
        public UserDetails GetUserDetailsByEmail(string strEmail)
        {            
            log.Info(">>>Get User District by Email method entered");
            UserDetails userDetails = dbContext.Users.Where(m => m.EmailId == strEmail).FirstOrDefault();        
            log.Debug(">>>Get User District by Email exited. ");
            return userDetails;
        }

        public List<UserComplaintModel> GetUserComplaintDetails(int userId,string status)
        {
            log.Info(">>>Get User District by Email method entered");
            List<UserComplaintModel> lstUserComplaintDetails = dbContext.UserComplaint.Where(m => m.UserId == userId && m.Status==status).ToList();
            log.Debug(">>>Get User District by Email exited. ");
            return lstUserComplaintDetails;
        }
    }
}