using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleProductItemConfiguration : IEntityTypeConfiguration<SaleProductItem>
    {
        public void Configure(EntityTypeBuilder<SaleProductItem> builder)
        {
            builder.HasOne(s => s.Product)
                .WithMany(p => p.SaleProductsItems)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Sale)
                .WithMany(s => s.SaleProductItems)
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Status)
             .HasConversion<string>()
             .HasMaxLength(20);
        }
    }
}
