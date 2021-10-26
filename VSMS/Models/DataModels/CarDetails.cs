using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("CarDetails")]
    public class CarDetails
    {
        [key]
        public int Id{ get; set; }

        [ForeignKey("Car")]
        public int IdCar{ get; set; }

        [ForeignKey("Feature")]
        public int IdFeature{ get; set; }

        [DisplayName("Content")]
        public string Content { get; set; }

        public Car Car{ get; set; }
        public Feature Feature{ get; set; }
    }
}