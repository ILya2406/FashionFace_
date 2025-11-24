using FashionFace.Dependencies.HttpClient.Interfaces;

namespace FashionFace.Dependencies.HttpClient.Implementations;

public sealed class DangerousHttpClientBuilder : IDangerousHttpClientBuilder
{
    public System.Net.Http.HttpClient Build(
        string baseAddress
    )
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
            )
            {
                BaseAddress =
                    new(
                        baseAddress
                    ),
            };

        return
            httpClient;
    }
}