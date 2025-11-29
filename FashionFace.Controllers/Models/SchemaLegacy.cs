using System.Collections.Generic;

public sealed record LoginRequestLegacy(
    string Username,
    string Password
);

public sealed record LoginResponseLegacy
(
    string AccessToken,
    IDictionary<string, object> User
);

public sealed record RegisterRequestLegacy
(
    string TelegramId,
    string Username,
    string Role // "model" или "photographer"
);

public sealed record RegisterResponseLegacy
(
    string Username,
    string Password,
    string Role,
    string Message
);

public sealed record ProfileResponseLegacy
(
    string TelegramId,
    string Username,
    string Role,

    string? Name,
    string? City,
    string? PhotoUrl,
    string? Description,
    string? Tags,
    string? Instagram,
    string? Portfolio,

    // Модели
    string? Gender,
    string? Height,
    string? ShoeSize,
    string? Hair,
    string? Eyes,

    // Фотографы
    string? Studio,
    string? Specialization
);

public sealed record ProfileUpdateRequestLegacy
(
    string? Description,
    string? Tags,
    string? PhotoUrl,
    string? Instagram,
    string? Portfolio
);

public sealed record UploadPhotoResponseLegacy
(
    string Url,
    string Message
);

public sealed record BarterPlaceLegacy
(
    string Id,
    string Name,
    string Type,
    string Location,
    string Description,
    string PhotoUrl,
    string Offer,
    string Contact,
    bool Active = true
);

public sealed record BarterPlacesResponseLegacy
(
    int Total,
    IReadOnlyList<BarterPlaceLegacy> Places
);

public sealed record ChangePasswordRequestLegacy
(
    string Username,
    string OldPassword,
    string NewPassword
);

public sealed record ChangePasswordResponseLegacy
(
    string Message
);

public sealed record ShowcaseItemLegacy
(
    string Id,
    string? Name,
    string Type,
    string? City,
    string? Gender,
    string? CoverUrl,
    string? Tg,
    string? Insta,
    string? Portfolio,
    IReadOnlyList<string> Tags
);

public sealed record ShowcaseResponseLegacy
(
    bool Ok,
    int Total,
    int Count,
    IReadOnlyList<ShowcaseItemLegacy> Items
);