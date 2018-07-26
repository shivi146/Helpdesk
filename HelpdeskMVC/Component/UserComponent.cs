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
        readonly UserRepository userRepository;
        public UserComponent(UserRepository repository)
        {
            this.userRepository = repository;
        }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

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
                userRepository.SaveUserDetails(user);
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
                UserDetails userDetail = userRepository.CheckUserLogin(login);
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
                throw new HelpdeskException("some error occurred !");
            }
        }

    }
}