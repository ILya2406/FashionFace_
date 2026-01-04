using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.UserToUserInvitations;
using FashionFace.Facades.Users.Interfaces.UserToUserInvitations;
using FashionFace.Facades.Users.Models.UserToUserInvitations;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.UserToUserInvitations;

public sealed class UserToUserChatInvitationAcceptFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    ICreateRepository createRepository,
    ITransactionManager transactionManager
) : IUserToUserChatInvitationAcceptFacade
{
    public async Task<UserToUserChatInvitationAcceptResult> Execute(
        UserToUserChatInvitationAcceptArgs args
    )
    {
        var (userId, invitationId) = args;

        var userToUserChatInvitationCollection =
            genericReadRepository.GetCollection<UserToUserChatInvitation>();

        var userToUserChatInvitation =
            await
                userToUserChatInvitationCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == invitationId
                            && entity.TargetUserId == userId
                            && entity.Status == ChatInvitationStatus.Created
                    );

        if (userToUserChatInvitation is null)
        {
            throw exceptionDescriptor.NotFound<UserToUserChatInvitation>();
        }

        userToUserChatInvitation.Status =
            ChatInvitationStatus.Accepted;

        var chatId =
            Guid.NewGuid();

        var userToUserChatSettings =
            new UserToUserChatSettings
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                ChatType = UserToUserChatType.Direct,
            };

        var userToUserChatProfiles =
            new List<UserToUserChatApplicationUser>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    Status = ChatMemberStatus.Active,
                    LastReadAt = DateTime.UtcNow,
                    ApplicationUserId = userToUserChatInvitation.InitiatorUserId,
                    Type = ChatMemberType.Creator,

                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    Status = ChatMemberStatus.Active,
                    LastReadAt = DateTime.UtcNow,
                    ApplicationUserId = userToUserChatInvitation.TargetUserId,
                    Type = ChatMemberType.Member,

                },
            };

        var userToUserChat =
            new UserToUserChat
            {
                Id = chatId,
                CreatedAt = DateTime.UtcNow,
                Settings = userToUserChatSettings,
                UserCollection = userToUserChatProfiles,
            };

        using var transaction =
            await
                transactionManager.BeginTransaction();

        await
            updateRepository
                .UpdateAsync(
                    userToUserChatInvitation
                );

        await
            createRepository
                .CreateAsync(
                    userToUserChat
                );

        await
            transaction.CommitAsync();

        var result =
            new UserToUserChatInvitationAcceptResult(
                chatId,
                userToUserChatInvitation.InitiatorUserId
            );

        return
            result;
    }
}