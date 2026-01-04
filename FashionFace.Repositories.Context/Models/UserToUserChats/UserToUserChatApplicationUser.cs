using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models.UserToUserChats;

public sealed class UserToUserChatApplicationUser : EntityBase
{
    public required Guid ApplicationUserId { get; set; }
    public required Guid ChatId { get; set; }

    public required ChatMemberType Type { get; set; }
    public required ChatMemberStatus Status { get; set; }

    public required DateTime LastReadAt { get; set; }

    public ApplicationUser? ApplicationUser  { get; set; }
    public UserToUserChat? Chat { get; set; }
}