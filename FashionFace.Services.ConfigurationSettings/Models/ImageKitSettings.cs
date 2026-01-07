namespace FashionFace.Services.ConfigurationSettings.Models;

public sealed class ImageKitSettings
{
    public string PrivateKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public string UrlEndpoint { get; set; } = string.Empty;
    public string Folder { get; set; } = "/models";
}
