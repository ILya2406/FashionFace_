using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.MediaEntities;

public sealed class MediaAggregateTagConfiguration : EntityBaseConfiguration<MediaAggregateTag>
{
    public override void Configure(EntityTypeBuilder<MediaAggregateTag> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.MediaAggregateId
            )
            .HasColumnName(
                "MediaAggregateId"
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
            .Property(
                entity => entity.PositionIndex
            )
            .HasColumnName(
                "PositionIndex"
            )
            .HasColumnType(
                "double precision"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithMany(
                entity => entity.PortfolioMediaTagCollection
            )
            .HasForeignKey(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Tag
            )
            .WithMany(
                entity => entity.PortfolioMediaTagCollection
            )
            .HasForeignKey(
                entity => entity.TagId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}