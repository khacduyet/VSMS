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
            ViewBag.CatId = new SelectList(db.Categories,"Id","CateName");
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