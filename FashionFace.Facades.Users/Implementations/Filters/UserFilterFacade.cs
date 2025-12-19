using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Facades.Users.Models.Locations;
using FashionFace.Facades.Users.Models.Portfolios;
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
                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.FilterCriteriaLocation
                    )
                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.FilterCriteriaAppearanceTraits
                    )
                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.FilterCriteriaTagCollection
                    )
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == filterId
                    );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var filterCriteria =
            filter.FilterCriteria!;

        var filterFilterLocation =
            filterCriteria.FilterCriteriaLocation;

        var filterCriteriaAppearanceTraits =
            filterCriteria.FilterCriteriaAppearanceTraits;

        var filterCriteriaTagCollection =
            filterCriteria.FilterCriteriaTagCollection;

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

        if (filterCriteriaAppearanceTraits is not null)
        {
            userAppearanceTraitsResult =
                new(
                    filterCriteriaAppearanceTraits.SexType,
                    filterCriteriaAppearanceTraits.FaceType,
                    filterCriteriaAppearanceTraits.HairColorType,
                    filterCriteriaAppearanceTraits.HairType,
                    filterCriteriaAppearanceTraits.HairLengthType,
                    filterCriteriaAppearanceTraits.BodyType,
                    filterCriteriaAppearanceTraits.SkinToneType,
                    filterCriteriaAppearanceTraits.EyeShapeType,
                    filterCriteriaAppearanceTraits.EyeColorType,
                    filterCriteriaAppearanceTraits.NoseType,
                    filterCriteriaAppearanceTraits.JawType,
                    filterCriteriaAppearanceTraits.Height,
                    filterCriteriaAppearanceTraits.ShoeSize
                );
        }

        var tagList =
            filterCriteriaTagCollection
                .Select(
                    entity =>
                        new UserTagListItemResult(
                            entity.TagId,
                            entity.PositionIndex
                        )
                )
                .ToList();

        var result =
            new UserFilterFacadeResult(
                filter.Id,
                filter.Name,
                filter.PositionIndex,
                filterCriteria.TalentType,
                userLocationListItemResult,
                userAppearanceTraitsResult,
                tagList
            );

        return
            result;
    }
}