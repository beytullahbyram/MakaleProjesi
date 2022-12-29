using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Filter
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Kullanıcı kullanıcı=filterContext.HttpContext.Session["login"] as Kullanıcı;
            if (kullanıcı != null && kullanıcı.Admin == false)
            {
                filterContext.Result=new RedirectResult("/Home/Index");
            }
        }
    }
}