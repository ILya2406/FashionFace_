using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class ProfileConfiguration : EntityBaseConfiguration<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.ApplicationUserId)
            .HasColumnName("ApplicationUserId")
            .HasColumnType("uuid");

        builder
            .Property(entity => entity.GivenName)
            .HasColumnName("GivenName")
            .HasColumnType("varchar(32)");

        builder
            .Property(entity => entity.MiddleName)
            .HasColumnName("MiddleName")
            .HasColumnType("varchar(32)");

        builder
            .Property(entity => entity.FamilyName)
            .HasColumnName("FamilyName")
            .HasColumnType("varchar(32)");

        builder
            .HasOne(entity => entity.ApplicationUser)
            .WithOne(entity => entity.Profile)
            .HasForeignKey<Profile>(entity => entity.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}