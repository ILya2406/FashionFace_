using System;
using System.Collections.Generic;

namespace FashionFace.Controllers.Anonymous.Responses.Models.Profile;

public sealed record PublicProfileResponse(
    Guid Id,
    string Name,
    string? Description,
    string Type,
    string? City,
    string? TelegramUsername,
    string? InstagramUsername,
    string? Email,
    string? PortfolioUrl,
    string? CoverImageUrl,
    string? Gender,
    string? AgeCategory,
    List<string>? Tags,
    DateTime CreatedAt,
    AppearanceTraitsResponse? AppearanceTraits,
    List<string>? MediaUrls
);
