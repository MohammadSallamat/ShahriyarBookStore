using Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.ProductAgg;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "product");
        builder.HasIndex(b => b.Slug).IsUnique();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Slug)
            .IsRequired()
            .IsUnicode(false);

        builder.OwnsOne(b => b.SeoData, option =>
        {
            option.Property(x => x.MetaKeyWords)
               .HasColumnName("MetaKeyWords")
               .HasMaxLength(500);

            option.Property(x => x.MetaDescription)
              .HasColumnName("MetaDescription")
              .HasMaxLength(500);

            option.Property(x => x.MetaTitle)
              .HasColumnName("MetaTitle")
              .HasMaxLength(500);

            option.Property(x => x.IndexPage)
              .HasColumnName("MetaKeyWords");

            option.Property(x => x.Canonical)
              .HasColumnName("Canonical")
              .HasMaxLength(500);

            option.Property(x => x.Schema)
              .HasColumnName("Schema");
        });

        builder.OwnsMany(b => b.Images, options =>
        {
            options.ToTable("Images", "product");

            options.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(b => b.Specifications, options =>
        {
            options.ToTable("Specifications", "product");

            options.Property(x => x.Key)
            .IsRequired();

            options.Property(x => x.Value)
            .IsRequired();

        });



    }
}
