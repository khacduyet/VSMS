using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class DriveTestViewModel
    {
        public int Id { get; set; }
        public string CarName{ get; set; }
        public string MemberName{ get; set; }
        public string Note{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public byte Status{ get; set; }
    }
}