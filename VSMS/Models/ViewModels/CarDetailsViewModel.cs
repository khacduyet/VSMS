using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public int Id{ get; set; }
        public string CarName{ get; set; }
        public string Descriptions{ get; set; }
        public string ModelName{ get; set; }
        public string CategoryName{ get; set; }
        public string Engine{ get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public byte Status{ get; set; }
    }
}