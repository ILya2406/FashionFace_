using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.OutboxEntity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.OutboxEntity;

public sealed class UserToUserChatMessageSendNotificationOutboxConfiguration :
    EntityConfigurationBase<UserToUserChatMessageSendNotificationOutbox>
{
    public override void Configure(EntityTypeBuilder<UserToUserChatMessageSendNotificationOutbox> builder)
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
                entity => entity.MessageCreatedAt
            )
            .HasColumnName(
                "MessageCreatedAt"
            )
            .HasColumnType(
                "timestamp with time zone"
            )
            .IsRequired();

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
            .HasForeignKey<UserToUserChatMessageSendNotificationOutbox>(
                entity => entity.MessageId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}