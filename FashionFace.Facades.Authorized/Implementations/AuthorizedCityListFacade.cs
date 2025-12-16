using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Authorized.Args;
using FashionFace.Facades.Authorized.Interfaces;
using FashionFace.Facades.Authorized.Models;
using FashionFace.Facades.Base.Models;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Authorized.Implementations;

public sealed class AuthorizedCityListFacade(
    IGenericReadRepository genericReadRepository
) : IAuthorizedCityListFacade
{
    public async Task<ListResult<AuthorizedCityListItemResult>> Execute(
        AuthorizedCityListArgs args
    )
    {
        var cityCollection =
            genericReadRepository.GetCollection<City>();

        var cityList =
            await
                cityCollection
                    .Select(
                        entity =>
                            new AuthorizedCityListItemResult(
                                entity.Id,
                                entity.Country,
                                entity.Name
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<AuthorizedCityListItemResult>(
                cityList.Count(),
                cityList
            );

        return
            result;
    }
}