using System;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
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
                    Id = Guid.NewGuid(),
                    Name = "",
                };

            var landmark =
                new Landmark
                {
                    Id = Guid.NewGuid(),
                    Name = "",
                };

            var place =
                new Place
                {
                    Id = Guid.NewGuid(),
                    Street = "",
                    BuildingId = building.Id,
                    LandmarkId = landmark.Id,
                    Building = building,
                    Landmark = landmark,
                };

            filterFilterCriteria.FilterCriteriaLocation =
                new()
                {
                    Id = Guid.NewGuid(),
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
                Guid.NewGuid();

            var filterMaleTraits =
                filterAppearanceTraitsArgs.FilterMaleTraits is null
                    ? null
                    : new FilterCriteriaMaleTraits
                    {
                        Id = Guid.NewGuid(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,
                        FacialHairLengthType = filterAppearanceTraitsArgs.FilterMaleTraits.FacialHairLengthType,
                    };

            var filterFemaleTraits =
                filterAppearanceTraitsArgs.FilterFemaleTraits is null
                    ? null
                    : new FilterCriteriaFemaleTraits
                    {
                        Id = Guid.NewGuid(),
                        FilterCriteriaAppearanceTraitsId = appearanceTraitsId,
                        BustSizeType = filterAppearanceTraitsArgs.FilterFemaleTraits.BustSizeType,
                    };

            filterFilterCriteria.FilterCriteriaAppearanceTraits =
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
                    Height = filterAppearanceTraitsArgs.Height,
                    ShoeSize = filterAppearanceTraitsArgs.ShoeSize,

                    FilterMaleTraits = filterMaleTraits,
                    FilterFemaleTraits = filterFemaleTraits,
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