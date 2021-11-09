using System;
using System.Collections.Generic;
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
    public class OrderController : CommonController
    {
        private VSMS_Entities db;
        private Repository<Order> _order;
        private Repository<OrderDetails> _orderDetails;

        public OrderController()
        {
            db = new VSMS_Entities();
            _order = new Repository<Order>();
            _orderDetails = new Repository<OrderDetails>();
        }



        // GET: Admin/Order
        [CustomAuthorize("ADMIN")]
        public ActionResult Index()
        {
            var data = from od in db.OrderDetails
                       join c in db.Cars on od.CarId equals c.Id
                       join o in db.Orders on od.OrderId equals o.Id
                       join mb in db.Members on o.MemberId equals mb.Id
                       join ad in db.Admins on o.AdminId equals ad.Id
                       select new ListOrderViewModel
                       {
                           IdAdmin = ad.Id,
                           NameAdmin = ad.Name,
                           IdOrderDetail = od.Id,
                           IdOrder = o.Id,
                           CarName = c.CarName,
                           CreatedAt = o.CreatedAt,
                           FullName = mb.FullName,
                           IdCar = c.Id,
                           IdMember = mb.Id,
                           Quantity = od.Quantity,
                           Status = o.Status,
                           Total = (od.Quantity * c.Price)
                       };
            return View(data);
        }

        // Function get infomation order 
        public JsonResult getInfOrder(int id)
        {
            var data = (from od in db.OrderDetails
                        join c in db.Cars on od.CarId equals c.Id
                        join o in db.Orders on od.OrderId equals o.Id
                        join mb in db.Members on o.MemberId equals mb.Id
                        join ad in db.Admins on o.AdminId equals ad.Id
                        where o.Id == id
                        select new
                        {
                            IdAdmin = ad.Id,
                            IdOrderDetail = od.Id,
                            IdOrder = o.Id,
                            CarName = c.CarName,
                            CreatedAt = o.CreatedAt,
                            IdCar = c.Id,
                            IdMember = mb.Id,
                            Quantity = od.Quantity,
                            Status = o.Status,
                            Total = (od.Quantity * c.Price)
                        }).SingleOrDefault();
            var img = (from c in db.Cars
                       join ipd in db.ImageProductDetails on c.Id equals ipd.IdProduct
                       join ip in db.ImageProducts on ipd.IdImageProduct equals ip.Id
                       where c.Id == data.IdCar
                       select new
                       {
                           ImageId = ip.Id,
                           ImageName = ip.ImageName
                       }).ToList();
            var car = (from c in db.Cars
                       join m in db.Modes on c.ModeId equals m.Id
                       join ca in db.Categories on c.CatId equals ca.Id
                       where c.Id == data.IdCar
                       select new
                       {
                           ModeName = m.ModeName,
                           CateName = ca.CateName,
                           Year = m.Year,
                           Engine = c.Engine,
                           FuelType = c.FuelType,
                           Transmission = c.Transmission,
                           Price = c.Price
                       }).SingleOrDefault();
            var mem = db.Members.Find(data.IdMember);
            var staff = db.Admins.Find(data.IdAdmin);
            return Json(new {car = car, data = data, img = img, mem = mem, staff = staff }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyIndex()
        {
            var value = (Models.Admin)this.HttpContext.Session[Common.CommonConstants.USER_SESSION];
            var data = from od in db.OrderDetails
                       join c in db.Cars on od.CarId equals c.Id
                       join o in db.Orders on od.OrderId equals o.Id
                       join mb in db.Members on o.MemberId equals mb.Id
                       where o.AdminId == value.Id
                       select new ListOrderViewModel
                       {
                           IdOrderDetail = od.Id,
                           IdOrder = o.Id,
                           CarName = c.CarName,
                           CreatedAt = o.CreatedAt,
                           FullName = mb.FullName,
                           IdCar = c.Id,
                           IdMember = mb.Id,
                           Quantity = od.Quantity,
                           Status = o.Status,
                           Total = (od.Quantity * c.Price)
                       };
            return View(data);
        }
        // lấy ra tất cả dữ liệu của order

        public JsonResult GetAllData()
        {
            var data = from od in db.OrderDetails
                       join c in db.Cars on od.CarId equals c.Id
                       join o in db.Orders on od.OrderId equals o.Id
                       join mb in db.Members on o.MemberId equals mb.Id
                       select new ListOrderViewModel
                       {
                           IdOrderDetail = od.Id,
                           IdOrder = o.Id,
                           CarName = c.CarName,
                           CreatedAt = o.CreatedAt,
                           FullName = mb.FullName,
                           IdCar = c.Id,
                           IdMember = mb.Id,
                           Quantity = od.Quantity,
                           Status = o.Status,
                           Total = (od.Quantity * c.Price)
                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // view tao moi order
        public ActionResult CreateOrder(int id)
        {
            var mem = db.Members.Find(id);
            ViewBag.CatId = new SelectList(db.Categories, "Id", "CateName");
            return View(mem);
        }
        [HttpPost]
        public ActionResult CreateOrder(Member or, int Quantity, int CarId)
        {
            var value = (Models.Admin)this.HttpContext.Session[Common.CommonConstants.USER_SESSION];
            Order o = new Order();
            o.MemberId = or.Id;
            o.CreatedAt = DateTime.Now;
            o.Status = 0;
            o.AdminId = value.Id;
            db.Orders.Add(o);

            OrderDetails od = new OrderDetails();
            od.OrderId = o.Id;
            od.Quantity = Quantity;
            od.CarId = CarId;
            od.Status = 0;
            db.OrderDetails.Add(od);

            db.SaveChanges();
            return RedirectToAction("MyIndex");
        }

        [HttpPost]
        public JsonResult getCars(int? id)
        {
            var model = db.Cars.Where(x => x.CatId == id).ToList();
            return Json(model);
        }

        // set lai trang thai order khi thanh toan thanh cong
        public JsonResult UpdatePay(int id)
        {
            var dataOd = db.Orders
                        .Where(p => p.Id == id)
                        .SingleOrDefault();
            dataOd.Status = 1;
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePay(int id)
        {
            var odetails = db.OrderDetails.Where(x => x.OrderId == id).SingleOrDefault();
            db.OrderDetails.Remove(odetails);
            var dataOd = db.Orders
                        .Where(p => p.Id == id)
                        .SingleOrDefault();
            db.Orders.Remove(dataOd);
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}