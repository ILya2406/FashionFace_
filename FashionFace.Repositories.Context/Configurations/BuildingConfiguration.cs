using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class BuildingConfiguration : EntityBaseConfiguration<Building>
{
    public override void Configure(EntityTypeBuilder<Building> builder)
    {
        base.Configure(
            builder
        );

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
            .Property(
                entity => entity.Name
            )
            .HasColumnName(
                "Name"
            )
            .HasColumnType(
                "varchar(128)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Place
            )
            .WithOne(
                entity => entity.Building
            )
            .HasForeignKey<Building>(
                entity => entity.PlaceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}