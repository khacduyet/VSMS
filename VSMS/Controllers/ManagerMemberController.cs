using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.ViewModels;

namespace VSMS.Controllers
{
    public class ManagerMemberController : BaseController
    {
        VSMS_Entities db = new VSMS_Entities();
        // GET: ManagerMember
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile(int? id)
        {
            var c = db.Members.Find(id);
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "Id,FullName, BirthDay,Address,Phone")] ProfileViewModels mem)
        {
            if (ModelState.IsValid)
            {
                var c = db.Members.Find(mem.Id);
                c.FullName = mem.FullName;
                c.BirthDay = mem.BirthDay;
                c.Address = mem.Address;
                c.Phone = mem.Phone;
                db.SaveChanges();
                getAlert("Change Profile Successfully!", "success");
                return RedirectToAction("Index");
            }
            return View(mem);
        }

        public ActionResult ChangePassword(int? id)
        {
            var cus = db.Members.Find(id);
            return View(cus);
        }
        [HttpPost]
        public ActionResult ChangePassword(int id, string Oldpass, string PassWord, string Repass)
        {
            var cus = db.Members.Find(id);
            if (Oldpass != cus.PassWord)
            {
                getAlert("Old password incorrect!", "danger");
                getPassForm(Oldpass, PassWord, Repass);
                return View(cus);
            }
            else if (PassWord == "" || Repass == "")
            {
                getAlert("Please enter password fields!", "danger");
                getPassForm(Oldpass, PassWord, Repass);
                return View(cus);
            }
            else if (PassWord != Repass)
            {
                getAlert("Confirm password incorrect!", "danger");
                getPassForm(Oldpass, PassWord, Repass);
                return View(cus);
            }
            cus.PassWord = PassWord;
            db.SaveChanges();
            getAlert("Change Password Successfully!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Email(int? id)
        {
            var cus = db.Members.Find(id);
            return View(cus);
        }

        public ActionResult MyDriveTest(int? id)
        {
            var dt = db.DriveTests.Where(x=>x.IdMember == id && x.Status != 3).OrderByDescending(y=>y.Id);
            ViewBag.Car = (from c in db.Cars
                           join ipd in db.ImageProductDetails on c.Id equals ipd.IdProduct
                           join ip in db.ImageProducts on ipd.IdImageProduct equals ip.Id
                           where ip.Status == 1
                           select new GetImageCarViewModel
                           {
                               IdCar = c.Id,
                               CarName = c.CarName,
                               IdImage = ip.Id,
                               ImageName = ip.ImageName,
                               Status = ip.Status
                           }).ToList();
            return View(dt);
        }

        public JsonResult RemoveDriveTest(int? id)
        {
            var dt = db.DriveTests.Find(id);
            if (dt.Status == 0)
            {
                dt.Status = 3;
                db.SaveChanges();
                return Json(new { success = 0 }, JsonRequestBehavior.AllowGet);
            } else if (dt.Status == 1)
            {
                return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = 2 }, JsonRequestBehavior.AllowGet);
        }
    }
}