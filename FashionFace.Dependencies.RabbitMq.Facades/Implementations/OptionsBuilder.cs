using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FashionFace.Dependencies.RabbitMq.Facades.Implementations;

public sealed class OptionsBuilder
{
   public IOptions<TEntity> Build<TEntity>(
        TEntity settings,
        string section
    )
        where TEntity : class
    {
        var configurationBuilder =
            new ConfigurationBuilder();

        var configuration =
            configurationBuilder
                .Build();

        configuration
            .GetSection(
                section
            )
            .Bind(
                settings
            );

        var options =
            Options
                .Create(
                    settings
                );

        return
            options;
    }
}