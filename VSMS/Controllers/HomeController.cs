using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;

namespace VSMS.Controllers
{
    public class HomeController : Controller
    {
        VSMS_Entities db = new VSMS_Entities();
        public ActionResult Index()
        {
            if (TempData["cus"] != null)
            {
                TempData["info"] = "Hello " + TempData["cus"].ToString();
                ViewBag.Name = TempData["cus"].ToString();
            }
            if (TempData["loginFail"] != null)
            {
                TempData["error"] = TempData["loginFail"];
            }
            if (TempData["saveSc"] != null)
            {
                TempData["success"] = TempData["saveSc"];
            }
            if (TempData["SaveEr"] != null)
            {
                TempData["error"] = TempData["SaveEr"];
            }
            return View();
        }
        

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult ServicesItem()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            return View();
        }

        public ActionResult InventoryItem()
        {
            return View();
        }

        public ActionResult Blog()
        {
            var posts = db.Posts;
            ViewBag.tags = db.Tags.Take(10);
            return View(posts);
        }
        public ActionResult BlogSingle(int id)
        {
            var post = db.Posts.Find(id);
            ViewBag.tags = db.Tags.Take(10);
            ViewBag.posts = db.Posts;
            var pt = db.post_Tags.Where(x => x.PostId == id).FirstOrDefault();
            pt.selectedIdArray = pt.TagId.Split(',').ToArray();
            List<int> num = new List<int>();
            foreach (var item in pt.selectedIdArray)
            {
                num.Add(Int16.Parse(item));
            }
            List<Tags> listTag = new List<Tags>();
            foreach (var item in num)
            {
                var tags = db.Tags.Find(item);
                listTag.Add(tags);
            }
            ViewBag.tag = listTag;
            return View(post);
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}