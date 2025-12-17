using System;

using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class FilePathService : IFilePathService
{
    public string GetRelativePath(
        Guid fileId
    )
    {
        var fileName =
            fileId.ToString(
                "D"
            );

        ReadOnlySpan<string> parts =
            fileName.Split(
                '-'
            );

        var filePath =
            $"{parts[0]}/{parts[1]}/{parts[2]}/{fileName}";

        return
            filePath;
    }
}