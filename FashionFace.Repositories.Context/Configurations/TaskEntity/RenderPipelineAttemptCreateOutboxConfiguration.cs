using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.TaskEntity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.TaskEntity;

public sealed class RenderPipelineAttemptCreateTaskConfiguration :
    EntityConfigurationBase<RenderPipelineAttemptCreateTask>
{
    public override void Configure(EntityTypeBuilder<RenderPipelineAttemptCreateTask> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.InitiatorUserId
            )
            .HasColumnName(
                "InitiatorUserId"
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
                "RenderPipelineAttemptId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

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