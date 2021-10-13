using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;

namespace VSMS.Areas.Admin.Controllers
{
    public class PermissionsController : Controller
    {
        private VSMS_Entities db = new VSMS_Entities();

        // GET: Admin/Permissions
        public ActionResult Index()
        {
            return View(db.Permissions.ToList());
        }

        // GET: Admin/Permissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Permissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerId,PerName,Description,Status")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                db.Permissions.Add(permission);
                db.SaveChanges();
                @TempData["success"] = "Create new success!";
                return RedirectToAction("Index");
            }

            return View(permission);
        }

        // GET: Admin/Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // POST: Admin/Permissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerId,PerName,Description,Status")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permission).State = EntityState.Modified;
                db.SaveChanges();
                @TempData["success"] = "Successfully!";
                return RedirectToAction("Index");
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permission permission = db.Permissions.Find(id);
            if (permission.Status == false)
            {
                db.Permissions.Remove(permission);
                db.SaveChanges();
                @TempData["success"] = "Erase successfully!";
                return RedirectToAction("Index");
            }
            @TempData["error"] = "This right is not allowed to delete!";
            return View(permission);
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
