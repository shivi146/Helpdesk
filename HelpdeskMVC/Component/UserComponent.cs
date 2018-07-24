using HelpdeskMVC.Models;
using PasswordSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelpdeskMVC.Repository;
using System.Data.Entity.Validation;
using HelpdeskMVC.Controllers;
using log4net;
using MVCApplWithSql.Common;

namespace HelpdeskMVC.Component
{

    public class UserComponent
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        UserRepository uRepo = new UserRepository();
        ApplContext dbContext = new ApplContext();
        public bool saveUserDetails(UserDetails user)
        {
            log.Debug("### Inside saveUserDetails UserComponent");
            try
            {
                user.Password = PasswordStorage.CreateHash(user.Password);
                log.Info(">>>> Password Hashed");
                uRepo.SaveUserDetails(user);
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
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        
        public UserDetails loginUser(LoginModel login)
        {           
            try
            {
                UserDetails userDetail =  uRepo.checkUserLogin(login);                 
                if(PasswordStorage.VerifyPassword(login.Password, userDetail.Password))
                {
                    return userDetail;
                }
                else
                {
                    return null;
                }               
            }
            catch (Exception ex)
            {
                throw new HelpdeskException("some error occurred !");
            }            
        }

    }
}