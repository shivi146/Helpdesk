using HelpdeskMVC.Component;
using HelpdeskMVC.Models;
using HelpdeskMVC.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace HelpdeskMVC.App_Start
{
    public static class IOCConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();          
            container.RegisterType<UserComponent, UserComponent>();
            container.RegisterType<AccountComponent, AccountComponent>();
            container.RegisterType<HelpdeskComponent, HelpdeskComponent>();
            container.RegisterType<UserRepository, UserRepository>();
            container.RegisterType<AccountRepository, AccountRepository>();
            container.RegisterType<HelpdeskRepository, HelpdeskRepository>();
            container.RegisterType<ApplContext, ApplContext>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));        }
    }
}