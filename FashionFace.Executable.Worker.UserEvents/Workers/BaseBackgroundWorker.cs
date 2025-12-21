using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Workers;

public abstract class BaseBackgroundWorker<TWorker>(
    ILogger<TWorker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken cancellationToken
    )
    {
        var workerName =
            typeof(TWorker).Name;

        logger
            .LogInformation(
                $"{workerName} started"
            );

        while (!cancellationToken.IsCancellationRequested)
        {
            logger
                .LogInformation(
                    $"{workerName} cycle started"
                );

            await
                DoWorkAsync();

            logger
                .LogInformation(
                    $"{workerName} cycle ended"
                );

            var fiveSecondsTimeStamp =
                TimeSpan
                    .FromSeconds(
                        30
                    );

            await
                Task
                    .Delay(
                        fiveSecondsTimeStamp,
                        cancellationToken
                    );
        }

        logger
            .LogInformation(
                $"{workerName} stopped"
            );
    }

    protected abstract Task DoWorkAsync();
}