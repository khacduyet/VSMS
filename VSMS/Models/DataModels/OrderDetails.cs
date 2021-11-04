using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public int Id{ get; set; }

        [ForeignKey("Car")]
        public int CarId{ get; set; }

        [ForeignKey("Order")]
        public int OrderId{ get; set; }
        public int Quantity{ get; set; }
        public byte Status{ get; set; }
        public Order Order{ get; set; }
        public Car Car{ get; set; }

        public OrderDetails()
        {
        }

        public OrderDetails(int carId, int orderId, int quantity, byte status)
        {
            CarId = carId;
            OrderId = orderId;
            Quantity = quantity;
            Status = status;
        }
    }
}