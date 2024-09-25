using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FilterAttribute = System.Web.Mvc.FilterAttribute;
using IAuthorizationFilter = System.Web.Mvc.IAuthorizationFilter;

namespace EDF.Web.Models
{
    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string returnURL = filterContext.HttpContext.Request.RawUrl;
            var user = UserManager.User;
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" },
                    { "controller", "Login" },
                    { "returnUrl", returnURL}
                });
            }
        }
    }
}