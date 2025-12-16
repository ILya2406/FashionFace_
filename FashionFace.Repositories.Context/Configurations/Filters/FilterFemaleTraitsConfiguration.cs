using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterFemaleTraitsConfiguration : EntityBaseConfiguration<FilterFemaleTraits>
{
    public override void Configure(EntityTypeBuilder<FilterFemaleTraits> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterAppearanceTraitsId
            )
            .HasColumnName(
                "FilterAppearanceTraitsId"
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
                entity => entity.FilterAppearanceTraits
            )
            .WithOne(
                entity => entity.FilterFemaleTraits
            )
            .HasForeignKey<FilterFemaleTraits>(
                entity => entity.FilterAppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}