using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FashionFace.Common.Extensions.Implementations;

public static class TasksExtensions
{
    public static async Task ForEachAsync<T>(
        IEnumerable<T> source,
        int dop,
        Func<T, Task> body,
        CancellationToken cancellationToken
    )
    {
        using var semaphore = new SemaphoreSlim(
            dop,
            dop
        );
        var tasks = source.Select(
            async item =>
            {
                await semaphore.WaitAsync(
                    cancellationToken
                );
                try
                {
                    await body(
                        item
                    );
                }
                finally
                {
                    semaphore.Release();
                }
            }
        );

        await Task.WhenAll(
            tasks
        );
    }
}