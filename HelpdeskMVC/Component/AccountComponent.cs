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
    public class AccountComponent
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HelpdeskController));
        readonly AccountRepository accountRepository;
        public AccountComponent(AccountRepository acctRepository)
        {
            this.accountRepository = acctRepository;
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
                accountRepository.SaveUserDetails(user);
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
                UserDetails userDetail = accountRepository.CheckUserLogin(login);
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
    }
}