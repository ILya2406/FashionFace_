using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.TaskEntity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.TaskEntity;

public sealed class RenderPipelineAttemptCreateRequestTaskConfiguration :
    EntityConfigurationBase<RenderPipelineAttemptCreateRequestTask>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttemptCreateRequestTask> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.InitiatorUserId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptCreateRequestTask.InitiatorUserId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.RenderPipelineAttemptId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptCreateRequestTask.RenderPipelineAttemptId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ModelMediaAggregateId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptCreateRequestTask.ModelMediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ProductMediaAggregateId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptCreateRequestTask.ProductMediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.PoseReferenceMediaAggregateId
            )
            .HasColumnName(
                nameof(RenderPipelineAttemptCreateRequestTask.PoseReferenceMediaAggregateId)
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
                nameof(RenderPipelineAttemptCreateRequestTask.UserPrompt)
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PoseReferenceMediaAggregate
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptCreateRequestTask>(
                entity => entity.PoseReferenceMediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.ProductMediaAggregate
            )
            .WithOne()
            .HasForeignKey<RenderPipelineAttemptCreateRequestTask>(
                entity => entity.ProductMediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.ModelMediaAggregate
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ModelMediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.RenderPipelineAttempt
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.RenderPipelineAttemptId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.InitiatorUser
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.InitiatorUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}