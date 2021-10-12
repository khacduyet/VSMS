using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VSMS.Models
{
    public class VSMS_Entities : DbContext
    {
        public VSMS_Entities() : base("name=VSMSConnectString")
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Per_relationship> Per_Relationships { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Permission_details> Permission_Details { get; set; }
    }
}