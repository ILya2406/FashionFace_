using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class LandmarkConfiguration : EntityBaseConfiguration<Landmark>
{
    public override void Configure(EntityTypeBuilder<Landmark> builder)
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
                entity => entity.Landmark
            )
            .HasForeignKey<Landmark>(
                entity => entity.PlaceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}