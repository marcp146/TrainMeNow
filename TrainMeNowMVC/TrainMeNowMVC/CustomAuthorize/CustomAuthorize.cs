using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainMeNowMVC.CustomAuthorize
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private readonly int[] allowedroles;
        private bool authorize;
        public CustomAuthorize(params int[] roles)
        {
            this.allowedroles = roles;
            this.authorize = false;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            foreach (var role in allowedroles)
            {
                if (HttpContext.Current.Session["RoleId"] != null)
                {
                    if ((int)HttpContext.Current.Session["RoleId"] == role)
                    {
                        authorize = true;
                        break;
                    }
                    else
                    {
                        authorize = false;
                    }
                }
                else
                {
                    authorize = false;
                    break;
                }

            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }
    }
}