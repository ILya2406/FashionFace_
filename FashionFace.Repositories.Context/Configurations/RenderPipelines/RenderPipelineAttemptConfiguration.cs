using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class RenderPipelineAttemptConfiguration : EntityConfigurationBase<RenderPipelineAttempt>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttempt> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.RenderPipelineId
            )
            .HasColumnName(
                nameof(RenderPipelineAttempt.RenderPipelineId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.RenderAttemptSettingsId
            )
            .HasColumnName(
                nameof(RenderPipelineAttempt.RenderAttemptSettingsId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.StartedAt
            )
            .HasColumnName(
                nameof(RenderPipelineAttempt.StartedAt)
            )
            .HasColumnType(
                "timestamp with time zone"
            );

        builder
            .Property(
                entity => entity.FinishedAt
            )
            .HasColumnName(
                nameof(RenderPipelineAttempt.FinishedAt)
            )
            .HasColumnType(
                "timestamp with time zone"
            );

        builder
            .Property(
                entity => entity.Status
            )
            .HasColumnName(
                nameof(RenderPipelineAttempt.Status)
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.ApplicationUser
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ApplicationUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.RenderPipeline
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.RenderPipelineId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.RenderAttemptSettings
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttempt>(
                entity => entity.RenderAttemptSettingsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}