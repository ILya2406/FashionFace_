namespace FashionFace.Dependencies.HttpClient.Interfaces;

public interface IDangerousHttpClientBuilder
{
    System.Net.Http.HttpClient Build(
        string baseAddress
    );
}