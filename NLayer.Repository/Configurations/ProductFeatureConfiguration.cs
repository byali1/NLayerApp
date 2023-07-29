using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Configurations
{
    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            //Ef Core zaten anlayacak, ogrenmek icin yazıyoruz

            builder.HasOne(x => x.Product)
                .WithOne(x => x.ProductFeature)
                .HasForeignKey<ProductFeature>(x => x.ProductId);
        }
    }
}
