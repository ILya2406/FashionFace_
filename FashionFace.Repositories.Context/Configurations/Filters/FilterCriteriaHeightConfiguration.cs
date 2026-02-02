using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaHeightConfiguration : EntityConfigurationBase<FilterCriteriaHeight>
{
    public override void Configure(EntityTypeBuilder<FilterCriteriaHeight> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterCriteriaAppearanceTraitsId
            )
            .HasColumnName(
                "FilterCriteriaAppearanceTraitsId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.FilterRangeValueId
            )
            .HasColumnName(
                "FilterRangeValueId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.FilterRangeValue
            )
            .WithOne()
            .HasForeignKey<FilterCriteriaHeight>(
                entity => entity.FilterRangeValueId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.FilterCriteriaAppearanceTraits
            )
            .WithOne(
                entity => entity.Height
            )
            .HasForeignKey<FilterCriteriaHeight>(
                entity => entity.FilterCriteriaAppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}