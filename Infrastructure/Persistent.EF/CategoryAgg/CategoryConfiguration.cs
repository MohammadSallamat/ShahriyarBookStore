using Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.CategoryAgg;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories","category");
        builder.HasIndex(b => b.Slug).IsUnique();

        builder.Property(x => x.Title)
          .IsRequired()
          .HasMaxLength(50);

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

        builder.HasMany(b => b.Childs)
            .WithOne()
            .HasForeignKey(b => b.ParentId);
        
        
    }
}
