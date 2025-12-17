using System;
using System.Threading.Tasks;

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

        if (talentType is not null)
        {
            filter.TalentType = talentType.Value;
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

            filter.FilterLocation =
                new()
                {
                    Id = Guid.NewGuid(),
                    FilterId = filterId,
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
                    : new FilterMaleTraits
                    {
                        Id = Guid.NewGuid(),
                        FilterAppearanceTraitsId = appearanceTraitsId,
                        FacialHairLengthType = filterAppearanceTraitsArgs.FilterMaleTraits.FacialHairLengthType,
                    };

            var filterFemaleTraits =
                filterAppearanceTraitsArgs.FilterFemaleTraits is null
                    ? null
                    : new FilterFemaleTraits
                    {
                        Id = Guid.NewGuid(),
                        FilterAppearanceTraitsId = appearanceTraitsId,
                        BustSizeType = filterAppearanceTraitsArgs.FilterFemaleTraits.BustSizeType,
                    };

            filter.FilterAppearanceTraits =
                new()
                {
                    Id = appearanceTraitsId,
                    FilterId = filterId,

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

        await
            updateRepository
                .UpdateAsync(
                    filter
                );
    }
}