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

        [ForeignKey("Member")]
        public int MemberId{ get; set; }
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public DateTime CreatedAt{ get; set; }
        public byte Status{ get; set; }

        public Member Member{ get; set; }
        public Admin Admin{ get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
        }

        public Order(int memberId, DateTime createdAt, byte status)
        {
            MemberId = memberId;
            CreatedAt = createdAt;
            Status = status;
        }
    }
}