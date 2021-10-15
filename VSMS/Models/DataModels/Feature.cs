using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Feature")]
    public class Feature
    {
        [Key]
        public int Id{ get; set; }

        [DisplayName("Name")]
        [StringLength(200)]
        public string Name{ get; set; }

        [DisplayName("Descriptions")]
        public string Descriptions{ get; set; }

        [DisplayName("Status")]
        public byte Status { get; set; }

        public ICollection<CarDetails> CarDetails{ get; set; }
    }
}