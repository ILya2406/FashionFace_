using System.Threading.Tasks;

using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Workers;

public sealed class FilterResultTalentValidationWorker(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IDeleteRepository deleteRepository,
    ILogger<FilterResultTalentValidationWorker> logger
) : BaseBackgroundWorker<FilterResultTalentValidationWorker>(
    logger
) {
    protected override async Task DoWorkAsync()
    {
        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            true
                    );

        if (filter is null)
        {
            return;
        }

        await
            updateRepository
                .UpdateAsync(
                    filter
                );
    }
}