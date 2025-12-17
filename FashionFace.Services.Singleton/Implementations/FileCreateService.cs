using System.IO;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class FileCreateService : IFileCreateService
{
    public async Task Create(
        FileCreateServiceArgs args
    )
    {
        var (
            sourceStream,
            filePath
            ) = args;

        var directory =
            Path
                .GetDirectoryName(
                    filePath
                );

        var isNotEmpty =
            directory.IsNotEmpty();

        if (isNotEmpty)
        {
            Directory
                .CreateDirectory(
                    directory
                );
        }

        await using var fileStream =
            new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                true
            );

        await
            sourceStream
                .CopyToAsync(
                    fileStream
                );

        await
            fileStream.FlushAsync();
    }
}