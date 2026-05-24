using Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SellerAgg;

internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers", "seller");


        builder.Property(x => x.NationalCode)
           .HasMaxLength(11)
           .IsRequired();

        builder.Property(x => x.ShopName)
           .HasMaxLength(50)
           .IsRequired();

        builder.OwnsMany(b => b.Inventories, option =>
        {
            option.ToTable("Inventories", "seller");

        });
    }
}
