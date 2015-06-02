using eP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<UserTypes> usertypes { get; set; }
        public DbSet<Compliance> compliance { get; set; }
        public DbSet<ProductCompliance> productcompliance { get; set; }
        public DbSet<ComplianceForm> ComplianceForms { get; set; }
        public DbSet<ProductsToCategoryModel> ProductsToCategoryModel { get; set; }
    }
}