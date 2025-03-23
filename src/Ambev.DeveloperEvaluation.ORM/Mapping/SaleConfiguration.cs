using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Cart)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Status)
               .HasConversion<string>()
               .HasMaxLength(20);

            builder.HasIndex(s => s.SaleNumber).IsUnique(true);
        }
    }
}
