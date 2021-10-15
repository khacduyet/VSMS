using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("CarDetails")]
    public class CarDetails
    {
        public int Id{ get; set; }
        public int IdCar{ get; set; }
        public int IdFeature{ get; set; }

        public string Content { get; set; }

        public Car Car{ get; set; }
        public Feature Feature{ get; set; }
    }
}