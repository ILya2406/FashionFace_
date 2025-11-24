using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.ConfigurationSettings.Models;

using Microsoft.Extensions.Options;

namespace FashionFace.Services.ConfigurationSettings.Implementations;

public sealed class CorsSettingsFactory(
    IOptions<CorsSettings> option
) : SettingsFactoryBase<CorsSettings>(
        option
    ),
    ICorsSettingsFactory;