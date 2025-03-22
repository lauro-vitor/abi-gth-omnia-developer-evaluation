using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartProductItemConfiguration : IEntityTypeConfiguration<CartProductItem>
    {
        public void Configure(EntityTypeBuilder<CartProductItem> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.Cart)
                .WithMany(p => p.CartProductItems)
                .HasForeignKey(p => p.CartId);
           
        }
    }
}
