using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using FashionFace.Controllers.Requests.Models;
using FashionFace.Controllers.Responses.Models;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Executable.Blazor.App.Models;
using FashionFace.Services.Singleton.Models;

using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Blazor.App.Services;

public sealed class AuthorizationService(
    ILogger<AuthorizationService> logger,
    HttpClient httpClient,
    ISerializationDecorator serializationDecorator
)
{
    public async Task<ApiResultContainer<LoginResponse>> Login(
        LoginRequest request
    )
    {
        var serializedJson =
            serializationDecorator
                .Serialize(
                    request
                );

        var content =
            new StringContent(
                serializedJson,
                Encoding.UTF8,
                "application/json"
            );

        var response =
            await
                httpClient
                    .PostAsync(
                        "api/v1/login",
                        content
                    );

        if (response.IsSuccessStatusCode)
        {
            var result =
                await
                    response
                        .Content
                        .ReadFromJsonAsync<LoginResponse>();

            return
                ApiResultContainer<LoginResponse>.Successful(result);
        }

        var contentJsonString =
            await
                response.Content.ReadAsStringAsync();

        logger.LogError(
            contentJsonString
        );

        var error =
            await
                response
                    .Content
                    .ReadFromJsonAsync<ErrorsContainerModel>();

        return
            ApiResultContainer<LoginResponse>.Failed(error);
    }
}