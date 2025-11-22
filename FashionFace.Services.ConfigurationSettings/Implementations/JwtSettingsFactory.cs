using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.ConfigurationSettings.Models;

using Microsoft.Extensions.Options;

namespace FashionFace.Services.ConfigurationSettings.Implementations;

public sealed class JwtSettingsFactory(
    IOptions<JwtSettings> option
) : SettingsFactoryBase<JwtSettings>(
        option
    ),
    IJwtSettingsFactory;