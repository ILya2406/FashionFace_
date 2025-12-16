using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.AppearanceTraits;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Facades.Users.Models.Locations;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFilterFacade
{
    public async Task<UserFilterFacadeResult> Execute(
        UserFilterArgs args
    )
    {
        var (
            userId,
            filterId
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .Include(entity => entity.FilterLocation)
                    .Include(entity => entity.FilterAppearanceTraits)
                    .Include(entity => entity.FilterTagCollection)
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == filterId
                    );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var filterFilterLocation =
            filter.FilterLocation;

        UserFilterLocationListItemResult? userLocationListItemResult = null;

        if (filterFilterLocation is not null)
        {
            var place =
                filterFilterLocation.Place;

            PlaceModel? placeModel = null;

            if (place is not null)
            {
                BuildingModel? buildingModel = null;

                if (place.Building is not null)
                {
                    buildingModel =
                        new(
                            place.Building.Name
                        );
                }

                LandmarkModel? landmarkModel = null;

                if (place.Landmark is not null)
                {
                    landmarkModel =
                        new(
                            place.Landmark.Name
                        );
                }

                placeModel =
                    new(
                        place.Street,
                        buildingModel,
                        landmarkModel
                    );
            }

            userLocationListItemResult =
                new(
                    filterFilterLocation.LocationType,
                    filterFilterLocation.CityId,
                    placeModel
                );
        }

        UserAppearanceTraitsResult? userAppearanceTraitsResult = null;

        if (filter.FilterAppearanceTraits is not null)
        {
            userAppearanceTraitsResult =
                new(
                    filter.FilterAppearanceTraits.SexType,
                    filter.FilterAppearanceTraits.FaceType,
                    filter.FilterAppearanceTraits.HairColorType,
                    filter.FilterAppearanceTraits.HairType,
                    filter.FilterAppearanceTraits.HairLengthType,
                    filter.FilterAppearanceTraits.BodyType,
                    filter.FilterAppearanceTraits.SkinToneType,
                    filter.FilterAppearanceTraits.EyeShapeType,
                    filter.FilterAppearanceTraits.EyeColorType,
                    filter.FilterAppearanceTraits.NoseType,
                    filter.FilterAppearanceTraits.JawType,
                    filter.FilterAppearanceTraits.Height,
                    filter.FilterAppearanceTraits.ShoeSize
                );
        }

        var tagList =
            filter
                .FilterTagCollection
                .Select(
                    entity => entity.TagId
                )
                .ToList();

        var result =
            new UserFilterFacadeResult(
                filter.Id,
                filter.Name,
                filter.PositionIndex,
                filter.TalentType,
                userLocationListItemResult,
                userAppearanceTraitsResult,
                tagList
            );

        return
            result;
    }
}