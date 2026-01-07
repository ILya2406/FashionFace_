using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Facades.Domains.Synchronization.Args;
using FashionFace.Facades.Domains.Synchronization.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Domains.Synchronization.Implementations;

public sealed class AppearanceTraitsDimensionsSynchronizationFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IDeleteRepository deleteRepository,
    ICreateRepository createRepository
) : IAppearanceTraitsDimensionsSynchronizationFacade
{

    public async Task SynchronizeAsync(
        AppearanceTraitsDimensionsSynchronizationArgs args
    )
    {
        var profileId =
            args.ProfileId;

        var appearanceTraitsCollection =
            genericReadRepository.GetCollection<AppearanceTraits>();

        var appearanceTraits =
            await
                appearanceTraitsCollection
                    .Include(
                        entity => entity.FemaleTraits
                    )
                    .Include(
                        entity => entity.MaleTraits
                    )
                    .FirstOrDefaultAsync(
                        entity => entity.ProfileId == profileId
                    );

        if (appearanceTraits is null)
        {
            throw exceptionDescriptor.NotFound<AppearanceTraits>();
        }

        var talentDimensionValueCollection =
            genericReadRepository.GetCollection<AppearanceTraitsDimensionValue>();

        var talentDimensionValueList =
            await
                talentDimensionValueCollection
                    .Include(
                        entity => entity.DimensionValue
                    )
                    .ThenInclude(
                        entity => entity.Dimension
                    )
                    .Where(
                        entity => entity.ProfileId == profileId
                    )
                    .ToListAsync();

        var dimensionValueTupleList =
            GetDimensionValueTupleList(
                appearanceTraits
            );

        foreach (var dimensionValueTuple in dimensionValueTupleList)
        {
            var dimensionTypeCode =
                dimensionValueTuple.Key;

            var newDimensionValueCode =
                dimensionValueTuple.Value;

            await
                UpdateDimensionValue(
                    talentDimensionValueList,
                    dimensionTypeCode,
                    newDimensionValueCode,
                    profileId
                );
        }
    }

    private static Dictionary<string, string> GetDimensionValueTupleList(AppearanceTraits appearanceTraits)
    {
        var dimensionValueTupleList =
            new Dictionary<string, string>
            {
                { AppearanceTraitsDimensionConstants.SexType, appearanceTraits.SexType.ToStringOrEmpty() },

                { AppearanceTraitsDimensionConstants.FaceType, appearanceTraits.FaceType.ToStringOrEmpty() },
                { AppearanceTraitsDimensionConstants.NoseType, appearanceTraits.NoseType.ToStringOrEmpty() },
                { AppearanceTraitsDimensionConstants.JawType, appearanceTraits.JawType.ToStringOrEmpty() },

                { AppearanceTraitsDimensionConstants.HairColorType, appearanceTraits.HairColorType.ToStringOrEmpty() },
                { AppearanceTraitsDimensionConstants.HairType, appearanceTraits.HairType.ToStringOrEmpty() },
                {
                    AppearanceTraitsDimensionConstants.HairLengthType, appearanceTraits.HairLengthType.ToStringOrEmpty()
                },

                { AppearanceTraitsDimensionConstants.BodyType, appearanceTraits.BodyType.ToStringOrEmpty() },
                { AppearanceTraitsDimensionConstants.SkinToneType, appearanceTraits.SkinToneType.ToStringOrEmpty() },

                { AppearanceTraitsDimensionConstants.EyeShapeType, appearanceTraits.EyeShapeType.ToStringOrEmpty() },
                { AppearanceTraitsDimensionConstants.EyeColorType, appearanceTraits.EyeColorType.ToStringOrEmpty() },
            };

        var hasMaleTraits =
            appearanceTraits.SexType == SexType.Male;

        if (hasMaleTraits)
        {
            dimensionValueTupleList
                .Add(
                    AppearanceTraitsDimensionConstants.FaceHairLengthType,
                    appearanceTraits.MaleTraits?.FacialHairLengthType.ToStringOrEmpty() ?? string.Empty
                );
        }

        var hasFemaleTraits =
            appearanceTraits.SexType == SexType.Female;

        if (hasFemaleTraits)
        {
            dimensionValueTupleList
                .Add(
                    AppearanceTraitsDimensionConstants.BustSizeType,
                    appearanceTraits.FemaleTraits?.BustSizeType.ToStringOrEmpty() ?? string.Empty
                );
        }

        return
            dimensionValueTupleList;
    }

    private async Task UpdateDimensionValue(
        IReadOnlyList<AppearanceTraitsDimensionValue> talentDimensionValueList,
        string dimensionTypeCode,
        string newDimensionValueCode,
        Guid profileId
    )
    {
        var talentDimensionValue =
            talentDimensionValueList
                .FirstOrDefault(
                    entity =>
                        entity
                            .DimensionValue!
                            .Dimension!
                            .Code
                        == dimensionTypeCode
                );

        var oldDimensionValueCode =
            talentDimensionValue?
                .DimensionValue?
                .Code;

        var isNotDifferent =
            newDimensionValueCode == oldDimensionValueCode;

        if (isNotDifferent)
        {
            return;
        }

        if (talentDimensionValue is not null)
        {
            await
                deleteRepository
                    .DeleteAsync(
                        talentDimensionValue
                    );
        }

        var isNotEmpty =
            newDimensionValueCode.IsNotEmpty();

        if (isNotEmpty)
        {
            var dimensionValueCollection =
                genericReadRepository.GetCollection<DimensionValue>();

            var dimensionValue =
                await
                    dimensionValueCollection
                        .FirstOrDefaultAsync(
                            entity =>
                                entity.Dimension!.Code == dimensionTypeCode
                                && entity.Code == newDimensionValueCode
                        );

            if (dimensionValue is null)
            {
                throw exceptionDescriptor.NotFound<DimensionValue>();
            }

            var newTalentDimensionValue =
                new AppearanceTraitsDimensionValue
                {
                    Id = Guid.NewGuid(),
                    DimensionValueId = dimensionValue.Id,
                    ProfileId = profileId,
                };

            await
                createRepository
                    .CreateAsync(
                        newTalentDimensionValue
                    );
        }
    }
}