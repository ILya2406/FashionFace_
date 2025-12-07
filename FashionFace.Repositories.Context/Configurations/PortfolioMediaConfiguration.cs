using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class PortfolioMediaConfiguration : EntityBaseConfiguration<PortfolioMedia>
{
    public override void Configure(EntityTypeBuilder<PortfolioMedia> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PortfolioId
            )
            .HasColumnName(
                "PortfolioId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

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
            .Property(
                entity => entity.SystemFileName
            )
            .HasColumnName(
                "SystemFileName"
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.OriginalFileName
            )
            .HasColumnName(
                "OriginalFileName"
            )
            .HasColumnType(
                "text"
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

        // validate empty withOne
        builder
            .HasOne(
                entity => entity.OriginalFile
            )
            .WithOne()
            .HasForeignKey<PortfolioMedia>(
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
            .HasForeignKey<PortfolioMedia>(
                entity => entity.OptimizedFileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Portfolio
            )
            .WithMany(
                entity => entity.PortfolioMediaCollection
            )
            .HasForeignKey(
                entity => entity.PortfolioId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}