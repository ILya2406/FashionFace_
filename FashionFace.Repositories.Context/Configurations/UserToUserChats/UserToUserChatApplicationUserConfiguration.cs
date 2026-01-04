using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.UserToUserChats;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.UserToUserChats;

public sealed class UserToUserChatApplicationUserConfiguration : EntityConfigurationBase<UserToUserChatApplicationUser>
{
    public override void Configure(EntityTypeBuilder<UserToUserChatApplicationUser> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ApplicationUserId
            )
            .HasColumnName(
                "ApplicationUserId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

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
                entity => entity.Status
            )
            .HasColumnName(
                "Status"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Type
            )
            .HasColumnName(
                "Type"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.LastReadMessagePositionIndex
            )
            .HasColumnName(
                "LastReadMessagePositionIndex"
            )
            .HasColumnType(
                "double precision"
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
                entity => entity.Chat
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ChatId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}