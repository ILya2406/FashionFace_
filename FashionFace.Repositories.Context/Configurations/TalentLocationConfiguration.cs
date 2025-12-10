using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class TalentLocationConfiguration : EntityBaseConfiguration<TalentLocation>
{
    public override void Configure(EntityTypeBuilder<TalentLocation> builder)
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
                entity => entity.TalentId
            )
            .HasColumnName(
                "TalentId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.CityId
            )
            .HasColumnName(
                "CityId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.LocationType
            )
            .HasColumnName(
                "LocationType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.PlaceId
            )
            .HasColumnName(
                "PlaceId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.City
            )
            .WithMany(
                entity => entity.TalentLocationCollection
            )
            .HasForeignKey(
                entity => entity.CityId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Place
            )
            .WithOne(
                entity => entity.TalentLocation
            )
            .HasForeignKey<TalentLocation>(
                entity => entity.PlaceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Talent
            )
            .WithMany(
                entity => entity.TalentLocationCollection
            )
            .HasForeignKey(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}