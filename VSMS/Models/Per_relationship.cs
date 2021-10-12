using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models
{
    [Table("Per_relationship")]
    public class Per_relationship
    {
        [Key, DisplayName("ID"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id_rel { get; set; }
        [DisplayName("ID Admin")]
        [ForeignKey("Admin")]
        public int Id_admin { get; set; }
        [DisplayName("ID Permission")]
        [ForeignKey("Permission")]
        public int Id_per { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime Date_created { get; set; }
        public Admin Admin { get; set; }
        public Permission Permission { get; set; }
    }
}