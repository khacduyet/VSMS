using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.Repository;

namespace VSMS.Controllers
{
    public class InventoryController : Controller
    {
        private VSMS_Entities db;
        private Repository<DriveTest> _driveTest;

        public InventoryController()
        {
            db = new VSMS_Entities();
            _driveTest = new Repository<DriveTest>();
        }

        // GET: Inventory
        public ActionResult Index()
        {
            
            return View();
        }
        // phương thức đăng ký driver test
        public JsonResult CreateDriverTest(DriveTest driveTest)
        {
            var data = _driveTest.Add(driveTest);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}