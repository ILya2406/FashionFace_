using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaMaleTraitsConfiguration : EntityBaseConfiguration<FilterCriteriaMaleTraits>
{
    public override void Configure(EntityTypeBuilder<FilterCriteriaMaleTraits> builder)
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
                entity => entity.FacialHairLengthType
            )
            .HasColumnName(
                "FacialHairLengthType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.FilterCriteriaAppearanceTraits
            )
            .WithOne(
                entity => entity.FilterMaleTraits
            )
            .HasForeignKey<FilterCriteriaMaleTraits>(
                entity => entity.FilterCriteriaAppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}