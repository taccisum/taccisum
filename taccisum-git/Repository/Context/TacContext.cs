using System;
using System.Data.Entity;
using Model.Entities;
using Model.Entity;

namespace Repository.Context
{
    public class TacContext : DbContext
    {

        public TacContext()
            : base("name=TacContext")
        {

        }

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<SysUserDemo> SysUserDemo { get; set; }
        public DbSet<Product> SysProducts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<Band> Bands { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'  
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded.   
            //Make sure the provider assembly is available to the running application.   
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.  
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }
    }
}