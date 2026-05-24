using Domain.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.RoleAgg;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "role");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsMany(b => b.Permissions, option =>
        {
            builder.ToTable("Permissions", "role");
        });
    }
}
