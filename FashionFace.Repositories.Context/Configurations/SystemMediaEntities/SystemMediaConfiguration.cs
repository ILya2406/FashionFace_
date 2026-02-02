using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.SystemMediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.SystemMediaEntities;

public sealed class SystemMediaConfiguration : EntityConfigurationBase<SystemMedia>
{
    public override void Configure(EntityTypeBuilder<SystemMedia> builder)
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

        builder
            .HasOne(
                entity => entity.OriginalFile
            )
            .WithOne()
            .HasForeignKey<SystemMedia>(
                entity => entity.OriginalFileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.OptimizedFile
            )
            .WithOne()
            .HasForeignKey<SystemMedia>(
                entity => entity.OptimizedFileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}
