using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VSMS.Models;

namespace VSMS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private VSMS_Entities db = new VSMS_Entities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (TempData.ContainsKey("userCurrent"))
                TempData["info"] = "Hello " + TempData["userCurrent"].ToString() + "!";
            return View();
        }

        public ActionResult Login()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Admin admin)
        {
            if (admin.Username != null && admin.Password != null)
            {
                // Get pass to MD5
                var passMD5 = Common.Common.ParseMD5(admin.Password);
                // Get the account to see if it exists or not
                var acc = db.Admins.SingleOrDefault(x => x.Username == admin.Username && x.Password == passMD5);
                if (acc != null)
                {
                    Session["admin"] = acc;
                    TempData["userCurrent"] = acc.Name;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Wrong account name or password!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}