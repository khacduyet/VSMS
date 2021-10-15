using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("DriveTest")]
    public class DriveTest
    {
        [Key]
        public int Id{ get; set; }

        [ForeignKey("Car")]
        public int IdCar{ get; set; }

        [ForeignKey("Member")]
        public int IdMember{ get; set; }

        [DisplayName("Note")]
        public string Note{ get; set; }
        public byte Status{ get; set; }
        public DateTime CreatedAt { get; set; }

        public Member Member{ get; set; }
        public Car Car{ get; set; }
    }
}