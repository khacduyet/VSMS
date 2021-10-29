using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VSMS.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (Models.Admin)Session[Common.CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new 
                    System.Web.Routing.RouteValueDictionary( new { Controller = "Admin", Action = "Login", Area = "Admin"}));
            }
            base.OnActionExecuting(filterContext);
        }
        // GET: Admin/Common
        public ActionResult GetSession()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login","Admin");
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }

    }
}