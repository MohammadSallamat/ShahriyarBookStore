using Domain.SiteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SiteAgg
{
    internal class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("Banners", "banner");

            builder.Property(x => x.ImageName)
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(x => x.Link)
            .IsRequired();
        }
    }
}
