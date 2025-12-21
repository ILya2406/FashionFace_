using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
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
                        entity => entity!.AppearanceTraits
                    )
                    .ThenInclude(
                        entity => entity!.Height
                    )
                    .ThenInclude(
                        entity => entity!.FilterRangeValue
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity!.AppearanceTraits
                    )
                    .ThenInclude(
                        entity => entity!.ShoeSize
                    )
                    .ThenInclude(
                        entity => entity!.FilterRangeValue
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity!.Location
                    )
                    .ThenInclude(
                        entity => entity!.Place
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.TagCollection
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity!.TagCollection
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
            filterCriteria.Location;

        var filterCriteriaAppearanceTraits =
            filterCriteria.AppearanceTraits;

        var filterCriteriaTagCollection =
            filterCriteria.TagCollection;

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

        UserFilterAppearanceTraitsResult? userAppearanceTraitsResult = null;

        if (filterCriteriaAppearanceTraits is not null)
        {
            var heightArgs =
                filterCriteriaAppearanceTraits
                    .Height?
                    .FilterRangeValue;

            var height =
                heightArgs is null
                    ? null
                    : new FilterRangeResult(
                        heightArgs.Min,
                        heightArgs.Max
                    );

            var shoeSizeArgs =
                filterCriteriaAppearanceTraits
                    .ShoeSize?
                    .FilterRangeValue;

            var shoeSize =
                shoeSizeArgs is null
                    ? null
                    : new FilterRangeResult(
                        shoeSizeArgs.Min,
                        shoeSizeArgs.Max
                    );

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
                    height,
                    shoeSize
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