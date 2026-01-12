using System;
using System.Collections.Generic;

namespace FashionFace.Facades.Users.Models.UserToUserChats;

public sealed record UserToUserChatListItemResult(
    Guid ChatId,
    IReadOnlyList<ChatUserInfoResult> UserList,
    int UnreadCount
);