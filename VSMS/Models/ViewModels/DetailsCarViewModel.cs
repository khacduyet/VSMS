using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class DetailsCarViewModel
    {
        public int IdProduct { get; set; }
        public int IdImageProduct { get; set; }
        public int IdDetails { get; set; }
        public int IdImage { get; set; }
        public string ImageName { get; set; }
        public byte StatusImg { get; set; }
    }
}