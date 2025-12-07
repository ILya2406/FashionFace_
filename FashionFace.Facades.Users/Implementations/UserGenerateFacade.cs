using System.Threading.Tasks;

using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Facades.Users.Implementations;

public sealed class UserGenerateFacade(
    INanoBananaService nanoBananaService,
    INanoBananaSettingsFactory nanoBananaSettingsFactory
) : IUserGenerateFacade
{
    public async Task<GenerateResult> Execute(
        UserGenerateArgs args
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