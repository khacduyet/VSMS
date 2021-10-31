using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN", "MOD")]
    public class PostsController : CommonController
    {
        private VSMS_Entities db = new VSMS_Entities();

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Admin);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Summary,Content,Image, AuthorId")] Post post, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        // Lưu ảnh theo đường dẫn
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Admin/Data/Blogs/"), Path.GetFileName(fileImage.FileName)));
                        // Gán đường dẫn cho trường Avatar
                        post.Image = "/Areas/Admin/Data/Blogs/" + fileImage.FileName;
                    }
                }
                post.CreatedAt = DateTime.Now;
                post.UpdatedAt = DateTime.Now;
                post.Status = true;
                db.Posts.Add(post);
                db.SaveChanges();
                @TempData["success"] = "Create new success!";
                return RedirectToAction("Index");
            }
            @TempData["error"] = "Create fail!";
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Summary,Content,Image")] Post post, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var posts = db.Posts.Find(post.Id);
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
                        posts.Image = "/Areas/Admin/Data/Images/" + fileImage.FileName;
                    }
                }
                else
                {
                    posts.Image = getAvt;
                }
                posts.Title = post.Title;
                posts.Summary = post.Summary;
                posts.Content = post.Content;
                posts.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                @TempData["success"] = "Edit success!";
                return RedirectToAction("Index");
            }
            @TempData["error"] = "Edit fail!";
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            @TempData["success"] = "Delete success!";
            return RedirectToAction("Index");
        }

        public ActionResult SetTags(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.postId = post.Id;
            var pt = db.post_Tags.Where(x => x.PostId == id).FirstOrDefault();
            pt.selectedIdArray = pt.TagId.Split(',').ToArray();
            ViewBag.Tags = new MultiSelectList(db.Tags, "Id", "Slug");
            return View(pt);
        }

        [HttpPost]
        public ActionResult SetTags(int PostId, post_tag pt)
        {
            var getpt = db.post_Tags.Where(x => x.PostId == PostId).FirstOrDefault();
            pt.TagId = string.Join(",", pt.selectedIdArray);
            if (getpt == null)
            {
                db.post_Tags.Add(pt);
            } else
            {
                getpt.TagId = pt.TagId;
            }
            db.SaveChanges();
            @TempData["success"] = "Set tags for post success!";
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
