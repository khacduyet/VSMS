using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VSMS.Models.ViewModels
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        VSMS_Entities db = new VSMS_Entities();
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var admin = (Models.Admin)httpContext.Session[Common.CommonConstants.USER_SESSION];
            if (admin != null)
            {
                var userRole = (from ad in db.Admins
                                join r in db.Per_Relationships on ad.Id equals r.Id_admin
                                join p in db.Permissions on r.Id_per equals p.PerId
                                where ad.Id == admin.Id
                                select new
                                {
                                    p.PerName
                                }).FirstOrDefault();
                foreach (var role in allowedroles)
                {
                    if (role == userRole.PerName) return true;
                }
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Common" },
                    { "action", "UnAuthorized" }
               });
        }
    }
}