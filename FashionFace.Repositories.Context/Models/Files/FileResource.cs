using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Files;

public sealed class FileResource : EntityBase,
    IWithCreatedAt,
    IWithIsDeleted,
    IWithRelativePath
{
    public required string RelativePath { get; set; }

    public required string FileName { get; set; }

    public required long SizeBytes { get; set; }

    public required string ContentType { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required bool IsDeleted { get; set; }

    public required string HashSha256 { get; set; }
}