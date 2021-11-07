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
        public ActionResult Index()
        {
            return View();
        }
        // lấy ra tất cả dữ liệu của order

        public JsonResult GetAllData()
        {
            var data = from od in db.OrderDetails
                       join c in db.Cars on od.CarId equals c.Id

                       join o in db.Orders on od.OrderId equals o.Id

                       join mb in db.Members on o.MemberId equals mb.Id

                       select new
                       {
                           IdOrder = o.Id,
                           IdMember = mb.Id,
                           MemberName = mb.FullName,
                           CarName = c.CarName,
                           Total = (c.Price * od.Quantity),
                           Status = od.Status

                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // view tao moi order
        public ActionResult CreateOrder(int id)
        {
            var mem = db.Members.Find(id);
            ViewBag.Member = mem;
            return View();
        }
        //Tạo mới một order do admin tạo
        public JsonResult CreateNewOrderRecord(OrderViewModel vmOrderViewModel)
        {
            DateTime today = DateTime.Today;
            var dataOrder = new Order { Total = 0, MemberId = vmOrderViewModel.MemberId, CreatedAt = today, Status = 0 };

            _order.SaveObject(dataOrder);

            var dataOrderDeTails = new OrderDetails { CarId = vmOrderViewModel.CarId, OrderId = dataOrder.Id, Quantity = vmOrderViewModel.Quantity, Status = 0 };

            _orderDetails.Add(dataOrderDeTails);

            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }


        // set lai trang thai order khi thanh toan thanh cong
        public JsonResult UpdatePay(int id)
        {

            var dataOd = db.OrderDetails
                        .Where(p => p.Id == id)
                        .SingleOrDefault();
            dataOd.Status = 1;
            db.SaveChanges();
            //Chạy
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }



    }
}