using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.Repository;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN", "MOD")]
    public class ManuafaturesController : CommonController
    {
        private VSMS_Entities db = new VSMS_Entities();

        private Repository<Manuafature> dbManua;

        public ManuafaturesController()
        {
            dbManua = new Repository<Manuafature>();
        }


        // GET: Admin/Manuafatures
        public ActionResult Index()
        {
            return View(db.Manuafatures.ToList());
        }

        public JsonResult GetAllData()
        {
            var data = dbManua.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Manuafatures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manuafature manuafature = db.Manuafatures.Find(id);
            if (manuafature == null)
            {
                return HttpNotFound();
            }
            return View(manuafature);
        }

        // GET: Admin/Manuafatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manuafatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Note,Status")] Manuafature manuafature)
        {
            if (ModelState.IsValid)
            {
                db.Manuafatures.Add(manuafature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manuafature);
        }

        // GET: Admin/Manuafatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manuafature manuafature = db.Manuafatures.Find(id);
            if (manuafature == null)
            {
                return HttpNotFound();
            }
            return View(manuafature);
        }

        // POST: Admin/Manuafatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Note,Status")] Manuafature manuafature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manuafature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manuafature);
        }

        // GET: Admin/Manuafatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manuafature manuafature = db.Manuafatures.Find(id);
            if (manuafature == null)
            {
                return HttpNotFound();
            }
            return View(manuafature);
        }

        // POST: Admin/Manuafatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manuafature manuafature = db.Manuafatures.Find(id);
            db.Manuafatures.Remove(manuafature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteManufacturer(int id)
        {
            Manuafature manuafature = db.Manuafatures.Find(id);
            db.Manuafatures.Remove(manuafature);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
