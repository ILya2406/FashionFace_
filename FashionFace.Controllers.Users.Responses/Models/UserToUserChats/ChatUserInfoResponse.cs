using System;

namespace FashionFace.Controllers.Users.Responses.Models.UserToUserChats;

public sealed record ChatUserInfoResponse(
    Guid UserId,
    string Name
);
