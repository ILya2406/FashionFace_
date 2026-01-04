using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Facades.Users.Models.UserToUserChats;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.UserToUserChats;

public sealed class UserToUserChatMessageListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserToUserChatMessageListFacade
{
    public async Task<ListResult<UserToUserChatMessageListItemResult>> Execute(
        UserToUserChatMessageListArgs args
    )
    {
        var (userId, chatId, offset, limit) = args;

        var userToUserChatCollection =
            genericReadRepository.GetCollection<UserToUserChat>();

        var userToUserChat =
            await
                userToUserChatCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == chatId
                            && entity
                                .UserCollection
                                .Any(
                                    profile =>
                                        profile.ApplicationUserId == userId
                                )
                    );

        if (userToUserChat is null)
        {
            throw exceptionDescriptor.NotFound<UserToUserChat>();
        }

        var userToUserChatMessageCollection =
            genericReadRepository.GetCollection<UserToUserChatMessage>();

        Expression<Func<UserToUserChatMessage, bool>> predicate =
            entity => entity.ChatId == chatId;

        var totalCount =
            await userToUserChatMessageCollection
                .CountAsync(
                    predicate
                );

        var userToUserChatMessageList =
            await
                userToUserChatMessageCollection
                    .Where(
                        predicate
                    )
                    .OrderByDescending(
                        entity => entity.CreatedAt
                    )
                    .Skip(
                        offset
                    )
                    .Take(
                        limit
                    )
                    .Select(
                        entity => entity.Message!
                    )
                    .Select(
                        entity =>
                            new UserToUserChatMessageListItemResult(
                                entity.ApplicationUserId,
                                new(
                                    entity.Id,
                                    entity.Value
                                )
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<UserToUserChatMessageListItemResult>(
                totalCount,
                userToUserChatMessageList
            );

        return
            result;
    }
}