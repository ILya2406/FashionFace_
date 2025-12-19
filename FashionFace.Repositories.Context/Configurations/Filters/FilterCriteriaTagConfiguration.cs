using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaTagConfiguration : EntityBaseConfiguration<FilterCriteriaTag>
{
    public override void Configure(EntityTypeBuilder<FilterCriteriaTag> builder)
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
                entity => entity.TagId
            )
            .HasColumnName(
                "TagId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.FilterCriteria
            )
            .WithMany(
                entity => entity.FilterCriteriaTagCollection
            )
            .HasForeignKey(
                entity => entity.FilterCriteriaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Tag
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.TagId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}