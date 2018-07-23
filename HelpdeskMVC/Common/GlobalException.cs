using log4net;
using MVCApplWithSql.Common;
//using MVCApplWithSql.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplWithSql
{
    public class GlobalException
    {
        public class HandleExceptionsAttribute : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext filterContext)
            {
                if (filterContext.Exception is HelpdeskException)
                {
                    var controllerName = filterContext.RouteData.Values["controller"].ToString();
                    var actionName = filterContext.RouteData.Values["action"].ToString();
                    var errormodel = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Custom",
                        MasterName = Master,
                        ViewData = new ViewDataDictionary(errormodel),
                        TempData = filterContext.Controller.TempData
                    };
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();


                }
            }
        }
    }
}