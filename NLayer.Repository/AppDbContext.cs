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
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override int SaveChanges()
        {
            UpdateChangeTracker();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateChangeTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                        {
                            Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                        case EntityState.Modified:
                        {
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;

                            entityReference.UpdatedDate = DateTime.Now;
                            break;
                        }
                    }
                }
            }
        }

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
