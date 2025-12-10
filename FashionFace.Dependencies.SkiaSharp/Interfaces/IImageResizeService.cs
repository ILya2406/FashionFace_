using System.IO;

namespace FashionFace.Dependencies.SkiaSharp.Interfaces;

public interface IImageResizeService
{
    byte[] ResizeToQuarter(
        MemoryStream inputStream
    );
}