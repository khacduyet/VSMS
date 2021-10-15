using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id{ get; set; }

        public float Total{ get; set; }

        [ForeignKey("Member")]
        public int MemberId{ get; set; }

        public DateTime CreatedAt{ get; set; }
        public byte Status{ get; set; }

        public Member Member{ get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<AdminOrder> AdminOrders{ get; set; }
    }
}