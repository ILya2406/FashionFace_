using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;
using FashionFace.Services.Singleton.Models;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class NanoBananaService : INanoBananaService
{
    private const string BaseUrl = "https://api.nanobananaapi.ai/api/v1/nanobanana";

    public async Task<string> GenerateImageAsync(
        GenerateImageArgs args
    )
    {
        var (
            apiKey,
            prompt,
            type,
            numImages,
            callBackUrl,
            watermark,
            imageUrls
            ) = args;

        var payload =
            new
            {
                prompt,
                type = type ?? "TEXTTOIAMGE",
                numImages = numImages ?? 1,
                callBackUrl,
                watermark,
                imageUrls,
            };

        HttpClient http = new();

        http.DefaultRequestHeaders.Add(
            "Authorization",
            $"Bearer {apiKey}"
        );

        var json =
            JsonSerializer.Serialize(
                payload
            );

        var stringContent =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

        var requestUri =
            $"{BaseUrl}/generate";

        var response =
            await
                http.PostAsync(
                    requestUri,
                    stringContent
                );

        var content =
            await
                response
                    .Content
                    .ReadAsStringAsync();

        var jsonSerializerOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

        var result =
            JsonSerializer
                .Deserialize<NanoBananaResult>(
                    content,
                    jsonSerializerOptions
                );

        if (!response.IsSuccessStatusCode || result?.Code != 200)
        {
            throw new(
                $"Generation failed: {result?.Msg ?? "Unknown error"}"
            );
        }

        return result.Data.TaskId;
    }

    public async Task<TaskStatusResponse?> GetTaskStatusAsync(
        TaskStatusArgs args
    )
    {
        var (
            apiKey,
            taskId
            ) = args;

        HttpClient http = new();

        http.DefaultRequestHeaders.Add(
            "Authorization",
            $"Bearer {apiKey}"
        );

        var requestUri =
            $"{BaseUrl}/record-info?taskId={taskId}";

        var response =
            await
                http
                    .GetAsync(
                        requestUri
                    );

        var content =
            await
                response
                    .Content
                    .ReadAsStringAsync();

        var jsonSerializerOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

        var result =
            JsonSerializer
                .Deserialize<TaskStatusResponse>(
                    content,
                    jsonSerializerOptions
                );

        return
            result;
    }
}