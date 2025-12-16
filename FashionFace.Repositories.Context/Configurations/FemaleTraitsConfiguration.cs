using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.AppearanceTraits;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class FemaleTraitsConfiguration : EntityBaseConfiguration<FemaleTraits>
{
    public override void Configure(EntityTypeBuilder<FemaleTraits> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.AppearanceTraitsId
            )
            .HasColumnName(
                "AppearanceTraitsId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.BustSizeType
            )
            .HasColumnName(
                "BustSizeType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.AppearanceTraits
            )
            .WithOne(
                entity => entity.FemaleTraits
            )
            .HasForeignKey<FemaleTraits>(
                entity => entity.AppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}