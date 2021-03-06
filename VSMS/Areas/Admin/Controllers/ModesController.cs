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
    public class ModesController : CommonController
    {
        private VSMS_Entities db = new VSMS_Entities();

        private Repository<Mode> dbMode;
        private Repository<Manuafature> dbManua;

        public ModesController()
        {
            dbMode = new Repository<Mode>();
            dbManua = new Repository<Manuafature>();
        }


        // GET: Admin/Modes
        public ActionResult Index()
        {
            var modes = db.Modes.Include(m => m.Manuafature);
            return View(modes.ToList());
        }

        public JsonResult GetAllData()
        {
            var _mode = dbMode.GetAll();
            return Json(_mode, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByManua(int id)
        {
            var data = dbMode.GetAll().Where(x => x.ManafatureId.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/Modes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode mode = db.Modes.Find(id);
            if (mode == null)
            {
                return HttpNotFound();
            }
            return View(mode);
        }

        // GET: Admin/Modes/Create
        public ActionResult Create()
        {
            ViewBag.ManafatureId = new SelectList(db.Manuafatures, "Id", "Name");
            return View();
        }

        // POST: Admin/Modes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModeName,Year,Note,ManafatureId,Status")] Mode mode)
        {
            if (ModelState.IsValid)
            {
                db.Modes.Add(mode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManafatureId = new SelectList(db.Manuafatures, "Id", "Name", mode.ManafatureId);
            return View(mode);
        }

        // GET: Admin/Modes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode mode = db.Modes.Find(id);
            if (mode == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManafatureId = new SelectList(db.Manuafatures, "Id", "Name", mode.ManafatureId);
            return View(mode);
        }

        // POST: Admin/Modes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModeName,Year,Note,ManafatureId,Status")] Mode mode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManafatureId = new SelectList(db.Manuafatures, "Id", "Name", mode.ManafatureId);
            return View(mode);
        }

        // GET: Admin/Modes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode mode = db.Modes.Find(id);
            if (mode == null)
            {
                return HttpNotFound();
            }
            return View(mode);
        }

        // POST: Admin/Modes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mode mode = db.Modes.Find(id);
            db.Modes.Remove(mode);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteModel(int id)
        {
            Mode mode = db.Modes.Find(id);
            db.Modes.Remove(mode);
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
