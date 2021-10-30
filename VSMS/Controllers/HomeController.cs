using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;

namespace VSMS.Controllers
{
    public class HomeController : Controller
    {
        VSMS_Entities db = new VSMS_Entities();
        public ActionResult Index()
        {
            if (TempData["cus"] != null)
            {
                TempData["info"] = "Hello " + TempData["cus"].ToString();
                ViewBag.Name = TempData["cus"].ToString();
            }
            if (TempData["loginFail"] != null)
            {
                TempData["error"] = TempData["loginFail"];
            }
            if (TempData["saveSc"] != null)
            {
                TempData["success"] = TempData["saveSc"];
            }
            if (TempData["SaveEr"] != null)
            {
                TempData["error"] = TempData["SaveEr"];
            }
            return View();
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult ServicesItem()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Inventory()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult InventoryItem()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult BlogSingle()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Shop()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Faq()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}