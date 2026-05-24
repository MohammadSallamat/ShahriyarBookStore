using Domain.SiteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SiteAgg;

internal class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {

        builder.ToTable("Sliders", "slider");

        builder.Property(x => x.ImageName)
       .IsRequired()
       .HasMaxLength(100);

        builder.Property(x => x.Link)
        .IsRequired();
    }
}
