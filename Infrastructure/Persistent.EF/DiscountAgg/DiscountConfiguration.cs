using Domain.DiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.EF.DiscountAgg;

internal class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts", "discount");

        builder.HasIndex(d => d.Code).IsUnique();

        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(d => d.DiscountAmount)
            .IsRequired();

        builder.Property(d => d.DiscountType)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired();
    }
}
