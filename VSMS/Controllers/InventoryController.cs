using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;

namespace VSMS.Controllers
{
    public class InventoryController : Controller
    {
        private VSMS_Entities db;

        public InventoryController()
        {
            db = new VSMS_Entities();
        }

        // GET: Inventory
        public ActionResult Index()
        {
            
            return View();
        }
    }
}