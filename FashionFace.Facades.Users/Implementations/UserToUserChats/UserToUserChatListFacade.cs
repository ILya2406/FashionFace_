using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Facades.Users.Models.UserToUserChats;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.UserToUserChats;

public sealed class UserToUserChatListFacade(
    IGenericReadRepository genericReadRepository
) : IUserToUserChatListFacade
{
    public async Task<ListResult<UserToUserChatListItemResult>> Execute(
        UserToUserChatListArgs args
    )
    {
        var (userId, offset, limit) = args;

        var userToUserChatCollection =
            genericReadRepository.GetCollection<UserToUserChat>();

        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        Expression<Func<UserToUserChat, bool>> predicate =
            entity =>
                entity
                    .UserCollection
                    .Any(
                        profile =>
                            profile.Status == ChatMemberStatus.Active
                            && profile.ApplicationUserId == userId
                    );

        var totalCount =
            await
                userToUserChatCollection
                    .CountAsync(
                        predicate
                    );

        var chatMessageCollection =
            genericReadRepository.GetCollection<UserToUserChatMessage>();

        var chatMemberCollection =
            genericReadRepository.GetCollection<UserToUserChatApplicationUser>();

        // Получаем чаты с ID участников и LastReadAt текущего пользователя
        var chatDataList =
            await
                userToUserChatCollection
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
                        entity =>
                            new
                            {
                                ChatId = entity.Id,
                                UserIds = entity
                                    .UserCollection
                                    .Select(
                                        profile => profile.ApplicationUserId
                                    )
                                    .ToList(),
                                CurrentUserLastReadAt = entity
                                    .UserCollection
                                    .Where(u => u.ApplicationUserId == userId)
                                    .Select(u => u.LastReadAt)
                                    .FirstOrDefault()
                            }
                    )
                    .ToListAsync();

        // Получаем ID чатов для подсчёта непрочитанных
        var chatIds = chatDataList.Select(c => c.ChatId).ToList();

        // Считаем непрочитанные сообщения для каждого чата
        var unreadCounts = await chatMessageCollection
            .Where(m => chatIds.Contains(m.ChatId))
            .GroupBy(m => m.ChatId)
            .Select(g => new
            {
                ChatId = g.Key,
                // Все сообщения в чате с их данными
                Messages = g.Select(m => new { m.ChatId, m.CreatedAt, m.Message!.ApplicationUserId })
            })
            .ToListAsync();

        // Создаём словарь для быстрого доступа к непрочитанным
        var unreadCountDict = new Dictionary<Guid, int>();
        foreach (var chatData in chatDataList)
        {
            var chatMessages = unreadCounts.FirstOrDefault(u => u.ChatId == chatData.ChatId);
            if (chatMessages != null)
            {
                // Считаем сообщения после LastReadAt, исключая свои собственные
                var count = chatMessages.Messages
                    .Count(m => m.CreatedAt > chatData.CurrentUserLastReadAt && m.ApplicationUserId != userId);
                unreadCountDict[chatData.ChatId] = count;
            }
            else
            {
                unreadCountDict[chatData.ChatId] = 0;
            }
        }

        // Собираем все уникальные ID пользователей
        var allUserIds =
            chatDataList
                .SelectMany(c => c.UserIds)
                .Distinct()
                .ToList();

        // Загружаем профили этих пользователей
        var profilesDict =
            await
                profileCollection
                    .Where(p => allUserIds.Contains(p.ApplicationUserId))
                    .ToDictionaryAsync(
                        p => p.ApplicationUserId,
                        p => p.Name
                    );

        // Формируем результат с именами и количеством непрочитанных
        var userToUserChatList =
            chatDataList
                .Select(
                    chatData =>
                        new UserToUserChatListItemResult(
                            chatData.ChatId,
                            chatData.UserIds
                                .Select(
                                    id =>
                                        new ChatUserInfoResult(
                                            id,
                                            profilesDict.GetValueOrDefault(id, "Пользователь")
                                        )
                                )
                                .ToList(),
                            unreadCountDict.GetValueOrDefault(chatData.ChatId, 0)
                        )
                )
                .ToList();

        var result =
            new ListResult<UserToUserChatListItemResult>(
                totalCount,
                userToUserChatList
            );

        return
            result;
    }
}