using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VSMS.Models.DataModels;

namespace VSMS.Models
{
    public class VSMS_Entities : DbContext
    {
        public VSMS_Entities() : base("name=VSMSConnectString")
        {

        }
        public DbSet<Manuafature> Manuafatures { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Car> Cars{ get; set; }
        public DbSet<CarDetails> CarDetails{ get; set; }
        public DbSet<Feature> Features{ get; set; } 
        public DbSet<ImageProduct> ImageProducts{ get; set; } 
        public DbSet<ImageProductDetails> ImageProductDetails{ get; set; } 
        public DbSet<Category> Categories{ get; set; } 
        public DbSet<DriveTest> DriveTests{ get; set; } 
        public DbSet<Member> Members{ get; set; } 
        public DbSet<Order> Orders{ get; set; } 
        public DbSet<OrderDetails> OrderDetails{ get; set; } 
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Per_relationship> Per_Relationships { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<post_tag> post_Tags { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}