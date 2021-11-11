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
            var checkExist = db.DriveTests.Where(x => x.IdCar == driveTest.IdCar && x.IdMember == driveTest.IdMember).SingleOrDefault();
            if (checkExist == null)
            {
                var data = _driveTest.Add(driveTest);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Uploads(int id)
        {
            var ipd = db.ImageProductDetails.Where(x => x.IdProduct == id);
            string src = "";
            foreach (var item in db.ImageProducts)
            {
                foreach (var i in ipd)
                {
                    if (item.Id == i.IdImageProduct && item.Status == 1)
                    {
                        src = item.ImageName;
                    }
                }
            }
            return Json(new { result = src },JsonRequestBehavior.AllowGet);
        }
    }
}