using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
       
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Çalışmış olduğum Assembly'i tara (IEntityTypeConfiguration)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature
            {
                Id = 1,
                Color = "Siyah",
                Height = 60,
                Width = 40, 
                ProductId = 1
            }, new ProductFeature
            {
                Id = 2,
                Color = "Brown",
                Height = 80,
                Width = 20,
                ProductId = 3
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
