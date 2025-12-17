using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Locations;

public sealed class UserLocationDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserLocationDeleteFacade
{
    public async Task Execute(
        UserLocationDeleteArgs args
    )
    {
        var (
            userId,
            locationId
            ) = args;

        var locationCollection =
            genericReadRepository.GetCollection<Location>();

        var location =
            await
                locationCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == locationId
                            && entity
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (location is null)
        {
            throw exceptionDescriptor.NotFound<Location>();
        }

        location.IsDeleted = true;

        await
            updateRepository
                .UpdateAsync(
                    location
                );
    }
}