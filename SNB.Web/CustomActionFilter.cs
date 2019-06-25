using SNB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SNB.Web
{
    // check session timeout===============================================================
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {


            var context = filterContext.HttpContext;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];
                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET&#95;SessionId") >= 0))
                    {
                        FormsAuthentication.SignOut();
                        string redirectTo = "~/User/Login";
                        if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            redirectTo = string.Format("~/User/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                        }
                        filterContext.HttpContext.Response.Redirect(redirectTo, true);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    // [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckConcurrentLogin : ActionFilterAttribute
    {
        private UserService _userService = new UserService();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var context = filterContext.HttpContext;
            if (context.Session["sessionid"] == null)
                context.Session["sessionid"] = "empty";

            //LoginsController loginsCtlr = new LoginsController();

            //string userId = HttpContext.Current.User.Identity.GetUserId();

            if (_userService.IsYourLoginStillTrue(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1], context.Session["sessionid"].ToString()))
            {
                if (!_userService.IsUserLoggedOnElsewhere(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1], context.Session["sessionid"].ToString()))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    // if it is being used elsewhere, update all their Logins records to LoggedIn = false, except for your session ID
                    _userService.LogEveryoneElseOut(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1], context.Session["sessionid"].ToString());
                    base.OnActionExecuting(filterContext);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary{{ "controller", "User" },
                                                  { "action", "Login" }

                                                });
            }

        }

    }
}