using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Profiles;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Profiles;

public sealed class ProfileConfiguration : EntityBaseConfiguration<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.IsDeleted
            )
            .HasColumnName(
                "IsDeleted"
            )
            .HasColumnType(
                "boolean"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ApplicationUserId
            )
            .HasColumnName(
                "ApplicationUserId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Name
            )
            .HasColumnName(
                "Name"
            )
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Description
            )
            .HasColumnName(
                "Description"
            )
            .HasColumnType(
                "varchar(1024)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.CreatedAt
            )
            .HasColumnName(
                "CreatedAt"
            )
            .HasColumnType(
                "timestamp with time zone"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.AgeCategoryType
            )
            .HasColumnName(
                "AgeCategoryType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.ApplicationUser
            )
            .WithOne(
                entity => entity.Profile
            )
            .HasForeignKey<Profile>(
                entity => entity.ApplicationUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}