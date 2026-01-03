using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.UserToUserChats;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.UserToUserChats;

public sealed class UserToUserChatMessageOutboxConfiguration : EntityBaseConfiguration<UserToUserChatMessageOutbox>
{
    public override void Configure(EntityTypeBuilder<UserToUserChatMessageOutbox> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ChatId
            )
            .HasColumnName(
                "ChatId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

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
                entity => entity.TargetUserId
            )
            .HasColumnName(
                "TargetUserId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.MessageId
            )
            .HasColumnName(
                "MessageId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.MessageValue
            )
            .HasColumnName(
                "MessageValue"
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.MessagePositionIndex
            )
            .HasColumnName(
                "PositionIndex"
            )
            .HasColumnType(
                "double precision"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.MessageCreatedAt
            )
            .HasColumnName(
                "CreatedAt"
            )
            .HasColumnType(
                "timestamp with time zone"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Status
            )
            .HasColumnName(
                "Status"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(16)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.AttemptCount
            )
            .HasColumnName(
                "AttemptCount"
            )
            .HasColumnType(
                "integer"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ProcessingStartedAt
            )
            .HasColumnName(
                "ProcessingStartedAt"
            )
            .HasColumnType(
                "timestamp with time zone"
            )
            .IsRequired();

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

        builder
            .HasOne(
                entity => entity.Chat
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ChatId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.TargetUser
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.TargetUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Message
            )
            .WithOne()
            .HasForeignKey<UserToUserChatMessageOutbox>(
                entity => entity.MessageId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}