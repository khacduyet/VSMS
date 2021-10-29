using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VSMS.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
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