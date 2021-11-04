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
    public class HomeController : Controller
    {
        VSMS_Entities db = new VSMS_Entities();
        List<Car> car = new List<Car>();
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
            var CarList = db.Cars.ToList();
            ViewBag.countResult = CarList.Count();
            var imgList = (from i in db.ImageProducts
                           join ipd in db.ImageProductDetails on i.Id equals ipd.IdImageProduct
                           where i.Status == 1
                           select new ListCarViewModel
                           {
                               IdProduct = ipd.IdProduct,
                               IdImageProduct = ipd.IdImageProduct,
                               ImageName = i.ImageName
                           }).ToList();
            ViewBag.ip = imgList;
            ViewBag.CatList = db.Categories;
            ViewBag.Cat = new SelectList(db.Categories, "Id", "CateName");
            ViewBag.Mode = new SelectList(db.Modes, "Id", "ModeName");
            return View(CarList);
        }

        public PartialViewResult FilterProduct(IEnumerable<Car> CarList, int? idCat, int? idModel, double x, double y, int? sortType)
        {
            if (sortType == 2)
            {
                CarList = db.Cars.OrderBy(s => s.CarName).ToList();
                if (idCat != null || idModel != null)
                {
                    CarList = db.Cars.Where(c => (c.CatId == idCat || c.ModeId == idModel) && (c.Price > x && c.Price < y)).OrderBy(s => s.CarName);
                }
            }
            else
            {
                CarList = db.Cars.OrderByDescending(s => s.CarName).ToList();
                if (idCat != null || idModel != null)
                {
                    CarList = db.Cars.Where(c => (c.CatId == idCat || c.ModeId == idModel) && (c.Price > x && c.Price < y)).OrderByDescending(s => s.CarName);
                }
            }

            ViewBag.countResult = CarList.Count();
            var imgList = (from i in db.ImageProducts
                           join ipd in db.ImageProductDetails on i.Id equals ipd.IdImageProduct
                           where i.Status == 1
                           select new ListCarViewModel
                           {
                               IdProduct = ipd.IdProduct,
                               IdImageProduct = ipd.IdImageProduct,
                               ImageName = i.ImageName
                           }).ToList();
            ViewBag.ip = imgList;
            ViewBag.Cat = new SelectList(db.Categories, "Id", "CateName", idCat);
            ViewBag.Mode = new SelectList(db.Modes, "Id", "ModeName", idModel);
            return PartialView("_PartialProduct", CarList);
        }

        public ActionResult InventoryItem(int id)
        {
            var car = db.Cars.Find(id);
            var imgList = from i in db.ImageProducts
                          join ipd in db.ImageProductDetails on i.Id equals ipd.IdImageProduct
                          where i.Status == 1 && ipd.IdProduct == id
                          select i.ImageName;
            ViewBag.ip = imgList.FirstOrDefault();
            ViewBag.ImageProduct = (from i in db.ImageProducts
                                    join ipd in db.ImageProductDetails on i.Id equals ipd.IdImageProduct
                                    where ipd.IdProduct == id
                                    select new DetailsCarViewModel
                                    {
                                        IdDetails = ipd.Id,
                                        IdProduct = ipd.IdProduct,
                                        IdImageProduct = ipd.IdImageProduct,
                                        IdImage = i.Id,
                                        ImageName = i.ImageName,
                                        StatusImg = i.Status
                                    }).ToList();
            ViewBag.Cars = db.Cars.ToList();
            var ImgL = (from i in db.ImageProducts
                        join ipd in db.ImageProductDetails on i.Id equals ipd.IdImageProduct
                        where i.Status == 1
                        select new ListCarViewModel
                        {
                            IdProduct = ipd.IdProduct,
                            IdImageProduct = ipd.IdImageProduct,
                            ImageName = i.ImageName
                        }).ToList();
            ViewBag.ipl = ImgL;

            // Get Feature
            var feature = (from ft in db.Features
                           join cd in db.CarDetails on ft.Id equals cd.IdFeature
                           where cd.IdCar == id
                           select new FeatureViewModel
                           {
                               Id = ft.Id,
                               Name = ft.Name
                           }).ToList();
            ViewBag.feature = feature;

            // Get Mode
            var mode = db.Modes.Where(x => x.Id == car.ModeId).FirstOrDefault();
            ViewBag.year = mode.Year;
            return View(car);
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
            ViewBag.tags = db.Tags.OrderByDescending(x => Guid.NewGuid()).Take(10);
            ViewBag.posts = db.Posts;

            if (db.post_Tags.Where(x => x.PostId == id).FirstOrDefault() != null)
            {
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
        [HttpPost]
        public JsonResult SubmitContact(Contact con)
        {
            if (con.Name.Length > 5 && con.Email.Length > 0 && con.Message.Length > 5)
            {
                db.Contacts.Add(con);
                db.SaveChanges();
                return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = 2 }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult compareSlide()
        {
            ViewBag.car = car;
            ViewBag.categories = db.Categories;
            ViewBag.model = db.Modes;
            ViewBag.feature = db.Features;
            ViewBag.cd = db.CarDetails;
            return PartialView("_PartialCompareSlide");
        }

        public void InsertCar(int id)
        {
            var c = db.Cars.Find(id);
            if (c != null)
            {
                car.Add(c);
                Session["carlist"] = car;
            }
        }
    }
}