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
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
                // Mã hóa mật khẩu & Thêm vào bảng admin
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
                @TempData["success"] = "Tạo mới thành công!";
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Name,Email,Avatar,Status")] Models.Admin admin, HttpPostedFileBase fileImage)
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
                    admin.Avatar = admin.Avatar;
                }
                // Cập nhật 
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                @TempData["success"] = "Sửa thành công!";
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
            if (admin.Id.Equals("1"))
            {
                db.Admins.Remove(admin);
                Models.Per_relationship pr = db.Per_Relationships.Where(x => x.Id_admin == id).FirstOrDefault();
                db.Per_Relationships.Remove(pr);
                db.SaveChanges();
                @TempData["success"] = "Xóa thành công!";
                return RedirectToAction("Index");
            }
            @TempData["error"] = "Không thể xóa tài khoản Admin!";
            return View(admin);
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
