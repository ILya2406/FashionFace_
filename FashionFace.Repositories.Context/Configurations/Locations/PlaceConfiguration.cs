using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Locations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Locations;

public sealed class PlaceConfiguration : EntityBaseConfiguration<Place>
{
    public override void Configure(EntityTypeBuilder<Place> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.BuildingId
            )
            .HasColumnName(
                "BuildingId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.LandmarkId
            )
            .HasColumnName(
                "LandmarkId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Street
            )
            .HasColumnName(
                "Street"
            )
            .HasColumnType(
                "varchar(128)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Building
            )
            .WithOne(
                entity => entity.Place
            )
            .HasForeignKey<Place>(
                entity => entity.BuildingId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Landmark
            )
            .WithOne(
                entity => entity.Place
            )
            .HasForeignKey<Place>(
                entity => entity.LandmarkId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}