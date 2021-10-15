using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("AdminOrder")]
    public class AdminOrder
    {
        [Key]
        public int Id{ get; set; }

        [ForeignKey("Orders")]
        public int OrderId{ get; set; }

        [ForeignKey("Admin")]
        public int AdminId{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public byte Status{ get; set; }

        public Order Orders{ get; set; }
        public Admin Admin{ get; set; }

    }
}