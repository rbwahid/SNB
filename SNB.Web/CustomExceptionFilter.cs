using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SNB.Web
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionSource = filterContext.Exception.Source,
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    ActionName = filterContext.RouteData.Values["action"].ToString(),
                    ExceptionLogTime = DateTime.Now
                };

                this.Log(filterContext.Exception);
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    { "controller", "ErrorHandler" }, { "action", "ExceptionError" }, {"ExceptionSource", logger.ExceptionSource},
                    {"ExceptionMessage", logger.ExceptionMessage}, {"ExceptionLogTime", logger.ExceptionLogTime},
                    { "ControllerName", logger.ControllerName}, {"ActionName", logger.ActionName}
                });
            }
        }

        private void Log(Exception exception)
        {
            //log exception here..
        }
    }
}