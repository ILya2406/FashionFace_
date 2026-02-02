using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class RenderPipelineAttemptSettingsConfiguration : EntityConfigurationBase<RenderPipelineAttemptSettings>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttemptSettings> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PoseReferenceProjectionId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSettings.PoseReferenceProjectionId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.UserPrompt
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSettings.UserPrompt)
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.UserPromptHash
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSettings.UserPromptHash)
            )
            .HasColumnType(
                "integer"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Temperature
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSettings.Temperature)
            )
            .HasColumnType(
                "double precision"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PoseReferenceProjection
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptSettings>(
                entity => entity.PoseReferenceProjectionId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}