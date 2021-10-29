using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private VSMS_Entities db = new VSMS_Entities();
        // GET: Admin/Admin
        
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (TempData.ContainsKey("userCurrent"))
                TempData["info"] = "Hello " + TempData["userCurrent"].ToString() + "!";
            return View();
        }

        public ActionResult MyProfile(int? id)
        {
            var ad = (Models.Admin)Session[Common.CommonConstants.USER_SESSION];
            id = ad.Id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        [HttpPost]
        public ActionResult MyProfile([Bind(Include = "Id,Username,Password,Name,Email,Avatar, Status")] Models.Admin admin, HttpPostedFileBase fileImg, string getAvt)
        {
            admin.Password = admin.Password;
            admin.ConfirmPassword = admin.Password;
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                // Kiểm tra extension của image
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImg != null)
                {
                    var ext = Path.GetExtension(fileImg.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImg.SaveAs(Path.Combine(Server.MapPath("~/Areas/Admin/Data/Images/"), Path.GetFileName(fileImg.FileName)));
                        admin.Avatar = "/Areas/Admin/Data/Images/" + fileImg.FileName;
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
                Session["admin"] = admin;
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        public ActionResult ChangePassword(int? id)
        {
            var ad = (Models.Admin)Session[Common.CommonConstants.USER_SESSION];
            if (ad != null)
            {
                id = ad.Id;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        [HttpPost]
        public ActionResult ChangePassword(int Id, string OldPass, string Password, string ConPass)
        {
            var admin = db.Admins.Find(Id);
            var op = Common.CommonConstants.ParseMD5(OldPass);
            if (!admin.Password.ToLower().Equals(op))
            {
                @TempData["error"] = "Old Password incorrect!";
                return View(admin);
            } else if (Password.Equals("") || ConPass.Equals(""))
            {
                @TempData["error"] = "Password field not null!";
                return View(admin);
            } else if (!Password.Equals(ConPass))
            {
                @TempData["error"] = "Confirm password incorrect!";
                return View(admin);
            }
            var pa = Common.CommonConstants.ParseMD5(Password);
            admin.Password = pa;
            admin.ConfirmPassword = pa;
            db.SaveChanges();
            @TempData["success"] = "Successfully!";
            Session["admin"] = admin;
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Models.Admin admin)
        {
            if (admin.Username != null && admin.Password != null)
            {
                // Get pass to MD5
                var passMD5 = Common.CommonConstants.ParseMD5(admin.Password);
                // Get the account to see if it exists or not
                var acc = db.Admins.SingleOrDefault(x => x.Username == admin.Username && x.Password == passMD5);
                if (acc != null)
                {
                    Session["admin"] = acc;
                    TempData["userCurrent"] = acc.Name;
                    Session.Add(Common.CommonConstants.USER_SESSION,acc);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Wrong account name or password!";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["admin"] = null;
            Session.Remove(Common.CommonConstants.USER_SESSION);
            return RedirectToAction("Login");
        }
    }
}