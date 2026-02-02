using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class RenderPipelineAttemptFailedResultConfiguration : EntityConfigurationBase<RenderPipelineAttemptFailedResult>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttemptFailedResult> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.RenderAttemptId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptFailedResult.RenderAttemptId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.RenderAttempt
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptFailedResult>(
                entity => entity.RenderAttemptId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}