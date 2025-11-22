namespace FashionFace.Dependencies.HttpClient.Interfaces;

public interface IDangerousHttpClient
{
    System.Net.Http.HttpClient GetClient();
}