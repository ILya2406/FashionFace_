using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor,
    IGuidGenerator guidGenerator
) : IUserFilterUpdateFacade
{
    public async Task Execute(
        UserFilterUpdateArgs args
    )
    {
        var (
            userId,
            filterId,
            name,
            positionIndex,
            talentType,
            filterLocationArgs,
            filterAppearanceTraitsArgs
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .Include(
                        entity => entity.FilterCriteria
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

        if (name is not null)
        {
            filter.Name = name;
        }

        if (positionIndex is not null)
        {
            filter.PositionIndex = positionIndex.Value;
        }

        var filterFilterCriteria =
            filter.FilterCriteria!;

        if (talentType is not null)
        {
            filterFilterCriteria.TalentType = talentType.Value;
        }

        if (filterLocationArgs is not null)
        {
            var building =
                new Building
                {
                    Id = guidGenerator.GetNew(),
                    Name = "",
                };

            var landmark =
                new Landmark
                {
                    Id = guidGenerator.GetNew(),
                    Name = "",
                };

            var place =
                new Place
                {
                    Id = guidGenerator.GetNew(),
                    Street = "",
                    BuildingId = building.Id,
                    LandmarkId = landmark.Id,
                    Building = building,
                    Landmark = landmark,
                };

            filterFilterCriteria.Location =
                new()
                {
                    Id = guidGenerator.GetNew(),
                    FilterCriteriaId = filterFilterCriteria.Id,
                    CityId = filterLocationArgs.CityId,
                    LocationType = filterLocationArgs.LocationType,
                    PlaceId = place.Id,
                    Place = place,
                };
        }

        if (filterAppearanceTraitsArgs is not null)
        {
            var appearanceTraitsId =
                guidGenerator.GetNew();

            var filterMaleTraits =
                filterAppearanceTraitsArgs.FilterMaleTraits is null
                    ? null
                    : new FilterCriteriaMaleTraits
                    {
                        Id = guidGenerator.GetNew(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,
                        FacialHairLengthType = filterAppearanceTraitsArgs.FilterMaleTraits.FacialHairLengthType,
                    };

            var filterFemaleTraits =
                filterAppearanceTraitsArgs.FilterFemaleTraits is null
                    ? null
                    : new FilterCriteriaFemaleTraits
                    {
                        Id = guidGenerator.GetNew(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,
                        BustSizeType = filterAppearanceTraitsArgs.FilterFemaleTraits.BustSizeType,
                    };

            var heightArgs =
                filterAppearanceTraitsArgs.Height;

            FilterCriteriaHeight? height = null;

            if (heightArgs is not null)
            {
                var filterRangeValue =
                    new FilterRangeValue
                    {
                        Id = guidGenerator.GetNew(),
                        Min = heightArgs.Min,
                        Max = heightArgs.Max,
                    };

                height =
                    new()
                    {
                        Id = guidGenerator.GetNew(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,

                        FilterRangeValue = filterRangeValue,
                        FilterRangeValueId = filterRangeValue.Id,
                    };
            }

            var shoeSizeArgs =
                filterAppearanceTraitsArgs.ShoeSize;

            FilterCriteriaShoeSize? shoeSize = null;

            if (shoeSizeArgs is not null)
            {
                var filterRangeValue =
                    new FilterRangeValue
                    {
                        Id = guidGenerator.GetNew(),
                        Min = shoeSizeArgs.Min,
                        Max = shoeSizeArgs.Max,
                    };

                shoeSize =
                    new()
                    {
                        Id = guidGenerator.GetNew(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,

                        FilterRangeValue = filterRangeValue,
                        FilterRangeValueId = filterRangeValue.Id,
                    };
            }

            filterFilterCriteria.AppearanceTraits =
                new()
                {
                    Id = appearanceTraitsId,
                    FilterCriteriaId = filterId,

                    SexType = filterAppearanceTraitsArgs.SexType,
                    FaceType = filterAppearanceTraitsArgs.FaceType,
                    HairType = filterAppearanceTraitsArgs.HairType,
                    HairColorType = filterAppearanceTraitsArgs.HairColorType,
                    HairLengthType = filterAppearanceTraitsArgs.HairLengthType,
                    EyeColorType = filterAppearanceTraitsArgs.EyeColorType,
                    EyeShapeType = filterAppearanceTraitsArgs.EyeShapeType,
                    BodyType = filterAppearanceTraitsArgs.BodyType,
                    SkinToneType = filterAppearanceTraitsArgs.SkinToneType,
                    NoseType = filterAppearanceTraitsArgs.NoseType,
                    JawType = filterAppearanceTraitsArgs.JawType,

                    Height = height,
                    ShoeSize = shoeSize,

                    MaleTraits = filterMaleTraits,
                    FemaleTraits = filterFemaleTraits,
                };
        }

        filter.Version += IntegerVersionConstants.VersionShift;

        await
            updateRepository
                .UpdateAsync(
                    filter
                );
    }
}