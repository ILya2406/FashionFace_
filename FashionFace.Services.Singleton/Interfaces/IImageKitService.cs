using System.Threading.Tasks;

namespace FashionFace.Services.Singleton.Interfaces;

public interface IImageKitService
{
    string PreprocessToJpeg(
        byte[] fileBytes,
        int maxWidth = 1280,
        int quality = 85
    );

    Task<string> UploadToImageKit(
        string filePath,
        string privateKey,
        string folder = "/models"
    );

    Task<string?> UploadPhotoBytes(
        byte[] fileBytes,
        string filename,
        string folder,
        string privateKey,
        int maxWidth = 1280,
        int quality = 85
    );
}