using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class RenderPipelineAttemptSucceededResultConfiguration : EntityConfigurationBase<RenderPipelineAttemptSucceededResult>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttemptSucceededResult> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.MediaAggregateId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSucceededResult.MediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.RenderAttemptId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptSucceededResult.RenderAttemptId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptSucceededResult>(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.RenderAttempt
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptSucceededResult>(
                entity => entity.RenderAttemptId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}