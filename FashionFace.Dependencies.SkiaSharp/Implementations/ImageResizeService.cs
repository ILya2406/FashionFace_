using System.IO;

using FashionFace.Dependencies.SkiaSharp.Interfaces;

using SkiaSharp;

namespace FashionFace.Dependencies.SkiaSharp.Implementations;

public sealed class ImageResizeService : IImageResizeService
{
    private const int Image8KWidth = 7680;
    private const int Image4KWidth = 4096;
    private const int Image2KWidth = 2048;
    private const int ImageHdWidth = 1920;

    private const int Image8KHeight = 4320;
    private const int Image4KHeight = 2160;
    private const int Image2KHeight = 1080;
    private const int ImageHdHeight = 1080;

    private const int Image8KDivider = 16;
    private const int Image4KDivider = 8;
    private const int Image2KDivider = 4;
    private const int ImageHdDivider = 2;

    public MemoryStream Optimize(
        Stream inputStream
    )
    {
        inputStream
            .Seek(
                0,
                SeekOrigin.Begin
            );

        // Copy stream first to avoid SKBitmap corrupting the original
        using var streamCopy = new MemoryStream();
        inputStream.CopyTo(streamCopy);
        streamCopy.Seek(0, SeekOrigin.Begin);

        using var original =
            SKBitmap
                .Decode(
                    streamCopy
                );

        // If decode fails, return a copy of original stream
        if (original == null)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            var fallbackStream = new MemoryStream();
            inputStream.CopyTo(fallbackStream);
            fallbackStream.Seek(0, SeekOrigin.Begin);
            return fallbackStream;
        }

        var divider = 1;
        if (original.Width > Image8KWidth || original.Height > Image8KHeight)
        {
            divider = Image8KDivider;
        }
        else if (original.Width > Image4KWidth || original.Height > Image4KHeight)
        {
            divider = Image4KDivider;
        }
        else if (original.Width > Image2KWidth || original.Height > Image2KHeight)
        {
            divider = Image2KDivider;
        }
        else if (original.Width > ImageHdWidth || original.Height > ImageHdHeight)
        {
            divider = ImageHdDivider;
        }

        var newWidth =
            original.Width / divider;

        var newHeight =
            original.Height / divider;

        var skImageInfo =
            new SKImageInfo(
                newWidth,
                newHeight
            );

        var skSamplingOptions =
            new SKSamplingOptions(
                SKFilterMode.Linear
            );

        using var resized =
            original
                .Resize(
                    skImageInfo,
                    skSamplingOptions
                );

        using var image =
            SKImage
                .FromBitmap(
                    resized
                );

        using var data =
            image
                .Encode(
                    SKEncodedImageFormat.Jpeg,
                    100
                );

        var byteArray =
            data.ToArray();

        var memoryStream =
            new MemoryStream(
                byteArray
            );

        return
            memoryStream;
    }
}