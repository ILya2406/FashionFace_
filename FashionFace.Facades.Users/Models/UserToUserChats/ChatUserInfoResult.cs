using System;

namespace FashionFace.Facades.Users.Models.UserToUserChats;

public sealed record ChatUserInfoResult(
    Guid UserId,
    string Name
);
