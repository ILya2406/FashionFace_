using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class AppearanceTraitsDimensionValueConfiguration : EntityBaseConfiguration<AppearanceTraitsDimensionValue>
{
    public override void Configure(EntityTypeBuilder<AppearanceTraitsDimensionValue> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.DimensionValueId
            )
            .HasColumnName(
                "DimensionValueId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ProfileId
            )
            .HasColumnName(
                "ProfileId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.DimensionValue
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.DimensionValueId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Profile
            )
            .WithMany(
                entity => entity.AppearanceTraitsDimensionValueCollection
            )
            .HasForeignKey(
                entity => entity.ProfileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasIndex(
                entity =>
                    new
                    {
                        entity.DimensionValueId,
                        entity.ProfileId,
                    }
            )
            .IsUnique();
    }
}