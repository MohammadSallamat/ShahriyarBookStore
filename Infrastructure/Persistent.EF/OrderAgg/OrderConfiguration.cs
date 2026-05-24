using Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.OrderAgg;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.OwnsOne(x => x.Discount, option =>
        {
            option.Property(y => y.Title)
            .HasMaxLength(50);
        });

        builder.OwnsOne(x => x.ShippingMethod, option =>
        {
            option.Property(y => y.ShippingType)
            .HasMaxLength(50);
        });

        builder.OwnsOne(x => x.Address, option =>
        {
            option.ToTable("Addresses", "order");

            option.Property(y => y.Shire)
            .HasMaxLength(50)
            .IsRequired();

            option.Property(y => y.City)
            .HasMaxLength(50)
            .IsRequired();

            option.Property(y => y.PostalCode)
            .HasMaxLength(40)
            .IsRequired();

            option.Property(y => y.PostalAddress)
            .HasMaxLength(50)
            .IsRequired();

            option.Property(y => y.Family)
            .HasMaxLength(50)
            .IsRequired();

            option.Property(y => y.Name)
            .HasMaxLength(50)
            .IsRequired();

            option.Property(y => y.NationalCode)
            .HasMaxLength(11)
            .IsRequired();

            option.Property(y => y.PhoneNumber)
           .HasMaxLength(11)
           .IsRequired();
        });

        builder.OwnsMany(x => x.Items, option =>
        {
            option.ToTable("Items", "order");
        });
    }
}
