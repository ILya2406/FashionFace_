using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using FashionFace.Services.Singleton.Interfaces;

using SkiaSharp;

public sealed class ImageKitUtils : IImageKitService
{
    private const string UploadUrl = "https://upload.imagekit.io/api/v1/files/upload";

    public string PreprocessToJpeg(byte[] fileBytes, int maxWidth = 1280, int quality = 85)
    {
        var tmpInput = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");
        File.WriteAllBytes(tmpInput, fileBytes);

        try
        {
            using var inputStream = File.OpenRead(tmpInput);
            using var bitmap = SKBitmap.Decode(inputStream);

            if (bitmap == null)
            {
                throw new Exception("Unable to decode image");
            }

            var w = bitmap.Width;
            var h = bitmap.Height;

            var resized = bitmap;

            if (w > maxWidth)
            {
                var ratio = (float)maxWidth / w;
                var newH = (int)(h * ratio);

                resized = new SKBitmap(maxWidth, newH);
                bitmap.ScalePixels(resized, new SKSamplingOptions(SKFilterMode.Linear));
            }

            var tmpOutput = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpg");

            using var outputStream = File.OpenWrite(tmpOutput);

            var jpegData = SKImage.FromBitmap(resized)
                .Encode(SKEncodedImageFormat.Jpeg, quality);

            jpegData.SaveTo(outputStream);

            return tmpOutput;
        }
        finally
        {
            try { File.Delete(tmpInput); } catch { }
        }
    }

    public async Task<string> UploadToImageKit(
        string filePath,
        string privateKey,
        string folder = "/models")
    {
        using var http = new HttpClient();
        using var form = new MultipartFormDataContent();

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var fileName = Path.GetFileName(filePath);

        var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

        form.Add(fileContent, "file", fileName);
        form.Add(new StringContent(fileName), "fileName");
        form.Add(new StringContent(folder), "folder");

        var basicAuth = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(privateKey + ":")
        );
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", basicAuth);

        var response = await http.PostAsync(UploadUrl, form);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            throw new Exception($"ImageKit error {response.StatusCode}: {err}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        if (!doc.RootElement.TryGetProperty("url", out var urlProp))
        {
            throw new Exception("ImageKit did not return URL");
        }

        return urlProp.GetString()!;
    }

    public async Task<string?> UploadPhotoBytes(
        byte[] fileBytes,
        string filename,
        string folder,
        string privateKey,
        int maxWidth = 1280,
        int quality = 85)
    {
        string jpegPath = null;

        try
        {
            Console.WriteLine($"📁 Optimizing: {filename}");

            jpegPath = PreprocessToJpeg(fileBytes, maxWidth, quality);

            Console.WriteLine($"🌐 Uploading to ImageKit...");

            var url = await UploadToImageKit(jpegPath, privateKey, folder);

            return url;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Upload error: {ex.GetType().Name}: {ex.Message}");
            return null;
        }
        finally
        {
            if (jpegPath != null)
            {
                try { File.Delete(jpegPath); } catch { }
            }
        }
    }
}