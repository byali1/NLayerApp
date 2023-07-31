using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        // App ayağa kalktığında default olusturulacak datalar
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "MSI Laptop 16 GB RAM RTX3050Ti",
                Price = 30000,
                Stock = 30,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 2,
                CategoryId = 2,
                Name = "Deri Ceket",
                Price = 200,
                Stock = 30,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 3,
                CategoryId = 3,
                Name = "Lambader",
                Price = 1000,
                Stock = 30,
                CreatedDate = DateTime.Now
            },
                new Product
                {
                    Id = 4,
                    CategoryId = 1,
                    Name = "iPhone XR",
                    Price = 10000,
                    Stock = 10,
                    CreatedDate = DateTime.Now
                });
        }
    }
}
