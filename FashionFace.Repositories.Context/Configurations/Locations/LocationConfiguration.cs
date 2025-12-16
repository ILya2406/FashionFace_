using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Locations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Locations;

public sealed class LocationConfiguration : EntityBaseConfiguration<Location>
{
    public override void Configure(EntityTypeBuilder<Location> builder)
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
                entity => entity.LocationCollection
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
                entity => entity.Location
            )
            .HasForeignKey<Location>(
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
                entity => entity.LocationCollection
            )
            .HasForeignKey(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}