using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Common;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private VSMS_Entities db = new VSMS_Entities();

        // GET: Admin/Account
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,ConfirmPassword,Name,Email,Avatar,Status")] Models.Admin admin, HttpPostedFileBase fileImage)
        {
            bool UserNameExist = false;
            if (ModelState.IsValid)
            {
                // Kiểm tra extension của image
                var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        // Lưu ảnh theo đường dẫn
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Admin/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        // Gán đường dẫn cho trường Avatar
                        admin.Avatar = "/Areas/Admin/Data/Images/" + fileImage.FileName;
                    }
                }
                // Kiểm tra trùng tên
                foreach (var item in db.Admins)
                {
                    if (item.Username.Equals(admin.Username))
                    {
                        UserNameExist = true;
                        break;
                    }
                }
                if (UserNameExist == false)
                {
                    //Mã hóa mật khẩu &Thêm vào bảng admin
                    admin.Password = Common.Common.ParseMD5(admin.Password);
                    admin.ConfirmPassword = Common.Common.ParseMD5(admin.ConfirmPassword);
                    db.Admins.Add(admin);
                    // Thêm mới quan hệ giữa Admin và Permission
                    Per_relationship pr = new Per_relationship();
                    pr.Id_admin = admin.Id;
                    pr.Id_per = 3;
                    pr.Date_created = DateTime.Now;
                    db.Per_Relationships.Add(pr);
                    db.SaveChanges();
                    // Thông báo bằng Toastr
                    @TempData["success"] = "Create new success!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Username", "This account has already existed!");
                    return View(admin);
                }

            }
            return View(admin);
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Name,Email,Avatar, Status")] Models.Admin admin, HttpPostedFileBase fileImage, string getAvt)
        {
            // Bỏ qua bước Validate mật khẩu
            admin.Password = admin.Password;
            admin.ConfirmPassword = admin.Password;
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                // Kiểm tra extension của image
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Admin/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        admin.Avatar = "/Areas/Admin/Data/Images/" + fileImage.FileName;
                    }
                }
                else
                {
                    admin.Avatar = getAvt;
                }
                admin.Status = admin.Status;
                // Cập nhật 
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                @TempData["success"] = "Successfully!";
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Admin admin = db.Admins.Find(id);
            if (!admin.Id.Equals(1))
            {
                db.Admins.Remove(admin);
                Models.Per_relationship pr = db.Per_Relationships.Where(x => x.Id_admin == id).FirstOrDefault();
                db.Per_Relationships.Remove(pr);
                db.SaveChanges();
                @TempData["success"] = "Erase successfully!";
                return RedirectToAction("Index");
            }
            @TempData["error"] = "Cannot delete Admin account!";
            return View(admin);
        }

        // Action New
        public ActionResult SetPermission(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Admin admin = db.Admins.Find(id);
            var getSelect = db.Per_Relationships.Where(x => x.Id_admin == id).FirstOrDefault();
            ViewBag.id = getSelect.Id_admin;
            ViewBag.Id_per = new SelectList(db.Permissions, "PerId", "PerName", getSelect.Id_per);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPermission([Bind(Include = "Id,Username,Name")] Models.Admin admin, int id, int Id_per)
        {
            var getSelect = db.Per_Relationships.SingleOrDefault(x => x.Id_admin.Equals(id));
            if (admin.Id != 1)
            {
                getSelect.Id_per = Id_per;
                db.SaveChanges();
                TempData["success"] = "Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.id = getSelect.Id_admin;
            ViewBag.Id_per = new SelectList(db.Permissions, "PerId", "PerName", getSelect.Id_per);
            TempData["error"] = "Don't change admin!";
            return View(admin);
        }

        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword ChangePassword, int id)
        {
            Models.Admin admin = db.Admins.Find(id);
            if (ModelState.IsValid)
            {
                admin.Password = Common.Common.ParseMD5(ChangePassword.Password);
                admin.ConfirmPassword = Common.Common.ParseMD5(ChangePassword.ConfirmPassword);
                db.SaveChanges();
                TempData["success"] = "Change Password Successfully!";
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        public ActionResult ChangeStatus(int id)
        {
            var ad = db.Admins.Find(id);
            ad.Name = ad.Name;
            ad.Username = ad.Username;
            ad.Password = ad.Password;
            ad.Email = ad.Email;
            ad.Avatar = ad.Avatar;
            ad.ConfirmPassword = ad.Password;
            ad.Status = !ad.Status;
            db.SaveChanges();
            TempData["info"] = "Change status successfully!";
            return RedirectToAction("Index");
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
