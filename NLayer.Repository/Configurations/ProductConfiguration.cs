using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();

            //XXXXXX,XX
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x=>x.CategoryId);
        }
    }
}
