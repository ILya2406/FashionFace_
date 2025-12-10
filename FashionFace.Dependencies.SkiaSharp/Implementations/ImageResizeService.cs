using System.IO;

using FashionFace.Dependencies.SkiaSharp.Interfaces;

using SkiaSharp;

namespace FashionFace.Dependencies.SkiaSharp.Implementations;

public sealed class ImageResizeService : IImageResizeService
{
    public byte[] ResizeToQuarter(
        MemoryStream inputStream
    )
    {
        using var original =
            SKBitmap
                .Decode(
                    inputStream
                );

        var newWidth =
            original.Width / 2;

        var newHeight =
            original.Height / 2;

        var skImageInfo =
            new SKImageInfo(
                newWidth,
                newHeight
            );

        var skSamplingOptions =
            new SKSamplingOptions(
                SKFilterMode.Nearest
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
                    90
                );

        var byteArray =
            data.ToArray();

        return
            byteArray;
    }
}