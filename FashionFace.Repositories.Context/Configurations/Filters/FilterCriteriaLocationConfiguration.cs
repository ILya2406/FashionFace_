using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaLocationConfiguration : EntityBaseConfiguration<FilterCriteriaLocation>
{
    public override void Configure(EntityTypeBuilder<FilterCriteriaLocation> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterCriteriaId
            )
            .HasColumnName(
                "FilterCriteriaId"
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
                entity => entity.FilterLocationCollection
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
                entity => entity.FilterLocation
            )
            .HasForeignKey<FilterCriteriaLocation>(
                entity => entity.PlaceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.FilterCriteria
            )
            .WithOne(
                entity => entity.FilterCriteriaLocation
            )
            .HasForeignKey<FilterCriteriaLocation>(
                entity => entity.FilterCriteriaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}