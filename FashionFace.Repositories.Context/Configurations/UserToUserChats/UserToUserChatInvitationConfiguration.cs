using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.UserToUserChats;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.UserToUserChats;

public sealed class UserToUserChatInvitationConfiguration : EntityConfigurationBase<UserToUserChatInvitation>
{
    public override void Configure(EntityTypeBuilder<UserToUserChatInvitation> builder)
    {
        base.Configure(
            builder
        );

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