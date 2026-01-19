using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.UserToUserChats;

namespace FashionFace.Repositories.Context.Models.OutboxEntity;

public sealed class UserToUserChatMessageSendNotificationOutbox : EntityBase, IOutbox
{
    public required Guid ChatId { get; set; }
    public required Guid InitiatorUserId { get; set; }
    public required Guid TargetUserId { get; set; }
    public required Guid MessageId { get; set; }

    public required string MessageValue { get; set; }
    public required DateTime MessageCreatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required Guid CorrelationId { get; set; }
    public required OutboxStatus OutboxStatus { get; set; }
    public required int AttemptCount { get; set; }
    public required DateTime? ClaimedAt { get; set; }

    public UserToUserChat? Chat { get; set; }
    public ApplicationUser? InitiatorUser { get; set; }
    public ApplicationUser? TargetUser { get; set; }
    public UserToUserChatMessage? Message { get; set; }
}