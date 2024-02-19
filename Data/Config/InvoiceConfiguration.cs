using Mataeem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mataeem.Data.Config
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {

            builder.Property(i => i.ShippingFee)
                .HasColumnType("decimal(18,2)");
            
            builder.Property(i => i.Taxes)
                .HasColumnType("decimal(18,2)");
            
            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
