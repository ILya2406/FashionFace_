using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models.UserToUserChats;

public sealed class UserToUserChatMessageOutbox : EntityBase
{
    public required Guid ChatId { get; set; }
    public required Guid InitiatorUserId { get; set; }
    public required Guid TargetUserId { get; set; }
    public required Guid MessageId { get; set; }

    public required string MessageValue { get; set; }
    public required double MessagePositionIndex { get; set; }
    public required DateTime MessageCreatedAt { get; set; }
    public required OutboxStatus Status { get; set; }
    public required int AttemptCount { get; set; }
    public required DateTime ProcessingStartedAt { get; set; }

    public UserToUserChat? Chat { get; set; }
    public ApplicationUser? InitiatorUser { get; set; }
    public ApplicationUser? TargetUser { get; set; }
    public  UserToUserChatMessage? Message { get; set; }
}