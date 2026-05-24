using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.UserAgg;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "user");

        builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(x => x.Family)
       .IsRequired()
       .HasMaxLength(50);

        builder.Property(x => x.Email)
       .IsRequired();

        builder.Property(x => x.AvatarName)
       .IsRequired()
       .HasMaxLength(50);

        builder.OwnsOne(b => b.PhoneNumber, option =>
        {
            option.Property(x => x.Value)
            .IsRequired()
            .HasColumnName("MobileNumber")
            .HasMaxLength(11);
        });

        builder.OwnsOne(b => b.Password, option =>
        {
            option.Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();

            option.Property(x => x.Salt)
            .HasColumnName("PasswordSalt")
            .IsRequired();
        });

        builder.OwnsMany(b => b.Roles, option =>
        {
            option.ToTable("Roles", "users");
        });

        builder.OwnsMany(b => b.UserAddresses, option =>
        {
            option.ToTable("UserAddresses", "users");

            option.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

            option.Property(x=>x.Family)
            .IsRequired()
            .HasMaxLength(50);

            option.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(50);

            option.Property(x => x.Province)
            .IsRequired()
            .HasMaxLength(50);

            option.Property(x => x.PostalAddress)
            .IsRequired();

            option.Property(x => x.PostalCode)
            .IsRequired();


            option.Property(x => x.NationalCode)
               .HasMaxLength(11)
               .IsRequired();

            option.OwnsOne(b => b.PhoneNumber, option =>
            {
                option.Property(x => x.Value)
                .IsRequired()
                .HasColumnName("MobileNumber")
                .HasMaxLength(11);
            });
            
        });

        builder.OwnsMany(b => b.Wallets, option =>
        {
            option.ToTable("Wallets", "users");
            option.Property(x => x.Description)
            .IsRequired();
        });

    }
}
