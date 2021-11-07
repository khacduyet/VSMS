using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class ListOrderViewModel
    {
        public int IdOrder { get; set; }
        public int IdMember { get; set; }
        public int? IdAdmin { get; set; }
        public string NameAdmin { get; set; }

        public int IdOrderDetail { get; set; }
        public int Quantity { get; set; }
        public string FullName { get; set; }
        public int IdCar { get; set; }
        public string CarName { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte Status { get; set; }
    }
}