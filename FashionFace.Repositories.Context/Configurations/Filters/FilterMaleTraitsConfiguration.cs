using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterMaleTraitsConfiguration : EntityBaseConfiguration<FilterMaleTraits>
{
    public override void Configure(EntityTypeBuilder<FilterMaleTraits> builder)
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
                entity => entity.FilterAppearanceTraits
            )
            .WithOne(
                entity => entity.FilterMaleTraits
            )
            .HasForeignKey<FilterMaleTraits>(
                entity => entity.FilterAppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}