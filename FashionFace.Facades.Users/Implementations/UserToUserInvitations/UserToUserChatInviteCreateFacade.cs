using System;
using FashionFace.Services.Singleton.Interfaces;
﻿using System.Threading.Tasks;

using FashionFace.Facades.Users.Args.UserToUserInvitations;
using FashionFace.Facades.Users.Interfaces.UserToUserInvitations;
using FashionFace.Facades.Users.Models.UserToUserInvitations;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FashionFace.Facades.Users.Implementations.UserToUserInvitations;

public sealed class UserToUserChatInvitationCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IDateTimePicker dateTimePicker
): IUserToUserChatInvitationCreateFacade
{
    public async Task<UserToUserChatInvitationCreateResult> Execute(
        UserToUserChatInvitationCreateArgs args
    )
    {
        var (userId, targetUserId) = args;

        var userToUserChatInvitationCollection =
            genericReadRepository.GetCollection<UserToUserChatInvitation>();

        var userToUserChatInvitation =
            await
                userToUserChatInvitationCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            (entity.TargetUserId == targetUserId
                                && entity.InitiatorUserId == userId)
                            || (entity.TargetUserId == userId
                                && entity.InitiatorUserId == targetUserId)
                    );

        if (userToUserChatInvitation is not null)
        {
            var existInvitationResult =
                new UserToUserChatInvitationCreateResult(
                    userToUserChatInvitation.Id,
                    userToUserChatInvitation.Status
                );

            return
                existInvitationResult;
        }

        var newUserToUserChatInvitation =
            new UserToUserChatInvitation
            {
                Id = Guid.NewGuid(),
                InitiatorUserId = userId,
                TargetUserId = targetUserId,
                Status = ChatInvitationStatus.Created,
                CreatedAt = dateTimePicker.GetUtcNow(),
            };

        await
            createRepository
                .CreateAsync(
                    newUserToUserChatInvitation
                );

        var result =
            new UserToUserChatInvitationCreateResult(
                newUserToUserChatInvitation.Id,
                newUserToUserChatInvitation.Status
            );

        return
            result;
    }
}
