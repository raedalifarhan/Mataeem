using Mataeem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mataeem.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(i => i.RegularPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.SellingPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
