using Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Grocery_Store.Areas.Admin.Data
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthor : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = (User)HttpContext.Current.Session["Account"];
            if (user.Account != null && user.Account.VaiTro == Roles)
                return true;
            return false;
        }
    }
}