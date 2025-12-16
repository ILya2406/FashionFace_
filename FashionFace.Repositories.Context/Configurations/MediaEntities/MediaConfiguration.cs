using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.MediaEntities;

public sealed class MediaConfiguration : EntityBaseConfiguration<Media>
{
    public override void Configure(EntityTypeBuilder<Media> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.OriginalFileId
            )
            .HasColumnName(
                "OriginalFileId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.OptimizedFileId
            )
            .HasColumnName(
                "OptimizedFileId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        // validate empty withOne
        builder
            .HasOne(
                entity => entity.OriginalFile
            )
            .WithOne()
            .HasForeignKey<Media>(
                entity => entity.OriginalFileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        // validate empty withOne
        builder
            .HasOne(
                entity => entity.OptimizedFile
            )
            .WithOne()
            .HasForeignKey<Media>(
                entity => entity.OptimizedFileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}