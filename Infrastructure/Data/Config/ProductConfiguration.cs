using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired(); 
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("Numeric(18,2)");
            builder.Property(p => p.PictureURL).IsRequired();
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(b => b.ProductBrandId);
            builder.HasOne(b => b.ProductType).WithMany().HasForeignKey(b => b.ProductTypeId);

        }
    }
}