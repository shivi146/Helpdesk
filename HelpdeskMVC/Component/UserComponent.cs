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

        /// <summary>
        /// password Hashing
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public bool SaveUserDetails(UserDetails user)
        {
            log.Debug("### Inside saveUserDetails UserComponent");
            try
            {
                user.Password = PasswordStorage.CreateHash(user.Password);
                user.UserRole = "U";
                log.Info(">>>> Password Hashed");
                userComplaintRepository.SaveUserDetails(user);
                log.Debug("### Exiting saveUserDetails UserComponent");
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        log.Error("Error Occured while saving user details" + ve.ErrorMessage);
                    }
                }
                throw new HelpdeskException("DB error occurred !!");
            }
        }

        /// <summary>
        /// Match passwords after hashing
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        public UserDetails LoginUser(LoginModel login)
        {
            try
            {
                UserDetails userDetail = userComplaintRepository.CheckUserLogin(login);
                if (PasswordStorage.VerifyPassword(login.Password, userDetail.Password))
                {
                    log.Info(">>>> Returning Userdetail After successful login for user" + userDetail.EmailId);
                    return userDetail;
                }
                else
                {
                    log.Info(">>>> Login unsuccessful");
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(">>> Some error occurred while logging user" + ex.Message);
                throw new Exception("some error occurred !");
            }
        }

        /// <summary>
        /// Getting the Application list from repository layer
        /// </summary>
        /// <returns>List of modules</returns>
        public List<ProjectApplications> GetApplicationName()
        {
           log.Debug("### GetApplicationName Entered");
           return userComplaintRepository.GetApplicationName();          
        }

        /// <summary>
        /// Getting the modules list from repository layer
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        public List<Modules> GetModuleName(int ApplicationId)
        {
            log.Debug("### GetModuleName Entered");
            return userComplaintRepository.GetModuleName(ApplicationId);
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