using Domain.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.EF.WishlistAgg;

internal class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlists", "user");

        builder.HasIndex(w => new { w.UserId, w.ProductId }).IsUnique();

        builder.Property(w => w.UserId)
            .IsRequired();

        builder.Property(w => w.ProductId)
            .IsRequired();
    }
}
