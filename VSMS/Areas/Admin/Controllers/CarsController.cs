using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;
using VSMS.Models.Repository;
using VSMS.Models.ViewModels;

namespace VSMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN", "MOD")]
    public class CarsController : CommonController
    {
        private VSMS_Entities db;
        private Repository<Car> _Car;
        private Repository<CarDetails> _carDetails;
        private Repository<ImageProduct> _ImageProduct;
        private Repository<ImageProductDetails> _ImageProDetails;

        public CarsController()
        {
            db = new VSMS_Entities();
            _Car = new Repository<Car>();
            _ImageProduct = new Repository<ImageProduct>();
            _ImageProDetails = new Repository<ImageProductDetails>();
            _carDetails = new Repository<CarDetails>();
        }


        // GET: Admin/Cars
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            var data = _Car.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(Car car,ImageProduct imgPro)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var _car = car;
                    _Car.SaveObject(_car);
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var timeName = DateTime.Now.ToString("HHmmss");
                        var imageName = timeName + file.FileName;

                        // Get the complete folder path and store the file inside it.  
                        file.SaveAs(Server.MapPath("~/Content/BackEnd/Uploads/product/") + imageName);
                        imgPro.ImageName = ("/Content/BackEnd/Uploads/product/" + imageName);

                        var imgProName = imgPro.ImageName;

                        // Thêm mới vào bảng ảnh sản phẩm
                        var imgProduct = new ImageProduct(imgProName,0);
                        _ImageProduct.SaveObject(imgProduct);

                        var imgDes = new ImageProductDetails(imgProduct.Id, _car.Id, 0);
                        _ImageProDetails.SaveObject(imgDes);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return RedirectToAction("Index");
        }


        // phương thức thêm mới tính năng cho ô tô
        public ActionResult CarFeature()
        {
            return View("CarFeature");
        }

        //  hiển thị một partial view
        public ActionResult CarDescriptionDetails()
        {
            var data = _Car.GetAll();
            return View(data);
        }
        // phương thức show dữ liệu detail lên modal của sản phẩm
        public JsonResult ShowDesModal(int id)
        {
            var data = _Car.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // json lấy data ảnh sản phẩm
        public JsonResult ShowImageCar(int idCar)
        {
            var data = from idet in db.ImageProductDetails
                       join c in db.Cars on idet.IdProduct equals c.Id

                       join ip in db.ImageProducts on idet.IdImageProduct equals ip.Id
                       where c.Id == idCar
                       select new
                       {
                           ImageName = ip.ImageName
                       };
            var cc = data;
            return Json(data, JsonRequestBehavior.AllowGet);           
        }

        // json lấy data tính năng của ô tô
        public JsonResult ShowFeatureCar(int idCar)
        {
            var data = from cd in db.CarDetails
                       join c in db.Cars on cd.IdCar equals c.Id

                       join ft in db.Features on cd.IdFeature equals ft.Id
                       where c.Id == idCar
                       select new
                       {
                           Id = ft.Id,
                           Name = ft.Name
                       };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // thêm tính năng cho ô tô
        public ActionResult CreateFeatureDetails(int idCar, int[] list) 
        {
            for (int i = 0; i < list.Length; i++)
            {
                int  IdFeature = list[i];
                // kiem tra trung id
                var idCheck = db.CarDetails.Where(x => x.IdFeature == IdFeature && x.IdCar == idCar).FirstOrDefault();
                if (idCheck != null)
                {
                    // @TempData["error"] = "Feature có mã bằng " + IdFeature + "đã tồn tại";
                    // return RedirectToAction("CarFeature");
                    // Chạy thử xem
                    return Json(new { thangdog = 1, IdFeature }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new CarDetails(idCar, IdFeature, "descript");
                    _carDetails.Add(data);
                }
               
            }
            return RedirectToAction("Index");
        }

        // chức năng xóa một tính năng của sản phẩm
        public ActionResult DeleteFeatureByCar(int idCar, int idFeature)
        {
            var data = db.CarDetails.Where(x => x.IdFeature == idFeature && x.IdCar == idCar).FirstOrDefault();
            var _del = _carDetails.Remove(data.Id);
            if (_del)
            {
                return Json(new { check = 1, idFeature }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

    }
}