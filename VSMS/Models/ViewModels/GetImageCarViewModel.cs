using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class GetImageCarViewModel
    {
        public int IdCar { get; set; }
        public string CarName { get; set; }

        public int IdImage { get; set; }

        public string ImageName { get; set; }

        public byte Status { get; set; }
    }
}