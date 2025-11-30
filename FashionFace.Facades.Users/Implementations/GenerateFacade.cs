using System.Threading.Tasks;

using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Facades.Users.Implementations;

public sealed class GenerateFacade(
    INanoBananaService nanoBananaService,
    INanoBananaSettingsFactory nanoBananaSettingsFactory
) : IGenerateFacade
{
    public async Task<GenerateResult> Execute(
        GenerateArgs args
    )
    {
        var nanoBananaSettings =
            nanoBananaSettingsFactory.GetSettings();

        var apiKey =
            nanoBananaSettings.ApiKey;

        var generateOptions =
            new GenerateImageArgs(
                apiKey,
                args.Prompt
            );

        var taskId =
            await
                nanoBananaService
                    .GenerateImageAsync(
                        generateOptions
                    );

        var result =
            new GenerateResult(
                taskId
            );

        return
            result;
    }
}