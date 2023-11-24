using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(t=> new {t.ProductId, t.CategoryId});

            builder.ToTable("ProductInCategory");

            builder.HasOne(t=>t.Product).WithMany(x=>x.ProductInCategories)
                .HasForeignKey(x=>x.ProductId);

            builder.HasOne(t => t.Category).WithMany(x => x.ProductInCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
