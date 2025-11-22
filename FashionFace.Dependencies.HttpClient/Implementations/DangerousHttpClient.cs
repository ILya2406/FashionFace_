using FashionFace.Dependencies.HttpClient.Interfaces;

namespace FashionFace.Dependencies.HttpClient.Implementations;

public sealed class DangerousHttpClient : IDangerousHttpClient
{
    public System.Net.Http.HttpClient GetClient()
    {
        var handler =
            new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };

        var httpClient =
            new System.Net.Http.HttpClient(
                handler
            );

        return
            httpClient;
    }
}