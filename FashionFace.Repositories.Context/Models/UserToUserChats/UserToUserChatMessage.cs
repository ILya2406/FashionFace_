using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.UserToUserChats;

public sealed class UserToUserChatMessage : EntityBase, IWithCreatedAt
{
    public required Guid MessageId { get; set; }
    public required Guid ChatId { get; set; }

    public required DateTime CreatedAt { get; set; }

    public UserToUserMessage? Message  { get; set; }
    public UserToUserChat? Chat { get; set; }
}