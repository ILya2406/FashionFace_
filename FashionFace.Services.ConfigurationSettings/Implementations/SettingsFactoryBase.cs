using FashionFace.Services.ConfigurationSettings.Interfaces;

using Microsoft.Extensions.Options;

namespace FashionFace.Services.ConfigurationSettings.Implementations;

public abstract class SettingsFactoryBase<TResult>(
    IOptions<TResult> option
) : ISettingsFactoryBase<TResult>
    where TResult : class
{
    public TResult GetSettings() =>
        option.Value;
}