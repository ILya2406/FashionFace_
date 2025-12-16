using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterTagConfiguration : EntityBaseConfiguration<FilterTag>
{
    public override void Configure(EntityTypeBuilder<FilterTag> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterId
            )
            .HasColumnName(
                "FilterMediaAggregateId"
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
                entity => entity.Filter
            )
            .WithMany(
                entity => entity.FilterTagCollection
            )
            .HasForeignKey(
                entity => entity.FilterId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Tag
            )
            .WithMany(
                entity => entity.FilterTagCollection
            )
            .HasForeignKey(
                entity => entity.TagId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}