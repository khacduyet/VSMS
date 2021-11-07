using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.Repository;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN", "MOD")]
    public class DriveTestController : CommonController
    {
        private VSMS_Entities db;
        private Repository<DriveTest> _drTest;

        public DriveTestController()
        {
            db = new VSMS_Entities();
            _drTest = new Repository<DriveTest>();
        }


        // GET: Admin/DriveTest
        public ActionResult Index()
        {
            var driveTest = from dt in db.DriveTests
                            join c in db.Cars on dt.IdCar equals c.Id
                            join m in db.Members on dt.IdMember equals m.Id
                            select new DriveTestViewModel
                            {
                                Id = dt.Id,
                                CarId = c.Id,
                                CarName = c.CarName,
                                MemberId = m.Id,
                                MemberName = m.FullName,
                                Note = dt.Note,
                                Status = dt.Status,
                                CreatedAt = dt.CreatedAt
                            };
            return View(driveTest);
        }

        public JsonResult changeStatus(int? id, byte? status)
        {
            var drive = db.DriveTests.Find(id);
            if (status == 0)
            {
                drive.Status = 1;
            } else if (status == 1)
            {
                drive.Status = 2;
            }
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllData()
        {
            var data = from dr in db.DriveTests
                       join c in db.Cars on dr.IdCar equals c.Id

                       join mb in db.Members on dr.IdMember equals mb.Id
                       select new DriveTestViewModel
                       {
                           Id = dr.Id,
                           CarName = c.CarName,
                           MemberName = mb.FullName,
                           Note = dr.Note,
                           CreatedAt = dr.CreatedAt,
                           Status = dr.Status
                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}