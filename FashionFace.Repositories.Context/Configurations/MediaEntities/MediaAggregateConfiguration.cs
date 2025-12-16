using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.MediaEntities;

public sealed class MediaAggregateConfiguration : EntityBaseConfiguration<MediaAggregate>
{
    public override void Configure(EntityTypeBuilder<MediaAggregate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PreviewMediaId
            )
            .HasColumnName(
                "PreviewMediaId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.OriginalMediaId
            )
            .HasColumnName(
                "OriginalMediaId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Description
            )
            .HasColumnName(
                "Description"
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PreviewMedia
            )
            .WithOne()
            .HasForeignKey<MediaAggregate>(
                entity => entity.PreviewMediaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.OriginalMedia
            )
            .WithOne()
            .HasForeignKey<MediaAggregate>(
                entity => entity.OriginalMediaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}