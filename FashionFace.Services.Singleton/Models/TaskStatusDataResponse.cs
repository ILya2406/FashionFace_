using System;

namespace FashionFace.Services.Singleton.Models;

public sealed record TaskStatusDataResponse(
    string TaskId,
    string ParamJson,
    DateTime CompletionTime,
    TaskResponse Response,
    int SuccessFlag,
    int? ErrorCode,
    string? ErrorMessage,
    string? CreateTime
);