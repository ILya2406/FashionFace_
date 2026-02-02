using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Dependencies.Redis.Args;
using FashionFace.Dependencies.Redis.Interfaces;
using FashionFace.Dependencies.Redis.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Context.Models.Talents;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterResultListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IFilterCursorCache filterCursorCache
) : IUserFilterResultListFacade
{
    public async Task<UserFilterResultListResult> Execute(
        UserFilterResultListArgs args
    )
    {
        var (
            userId,
            filterId,
            limit
            ) = args;

        var filter =
            await
                GetFilter(
                    filterId,
                    userId
                );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var talentCollection =
            genericReadRepository.GetCollection<Talent>();

        var queryable =
            talentCollection
                .Include(t => t.ProfileTalent)
                    .ThenInclude(pt => pt!.Profile)
                        .ThenInclude(p => p!.MediaFileCollection)
                .Include(t => t.ProfileTalent)
                    .ThenInclude(pt => pt!.Profile)
                        .ThenInclude(p => p!.AppearanceTraits)
                .AsQueryable();

        var criteria =
            filter.FilterCriteria;

        if (criteria is null)
        {
            throw exceptionDescriptor.NotFound<FilterCriteria>();
        }

        var criteriaTalentType =
            criteria.TalentType;

        if (criteriaTalentType is not null)
        {
            queryable =
                queryable
                    .Where(
                        entity =>
                            entity.TalentType == criteriaTalentType
                    );
        }

        var criteriaLocation =
            criteria.Location;

        if (criteriaLocation is not null)
        {
            queryable =
                queryable
                    .Where(
                        entity =>
                            entity
                                .LocationCollection
                                .Any(
                                    location =>
                                        location.LocationType == criteriaLocation.LocationType
                                        && location.CityId == criteriaLocation.CityId
                                )
                    );
        }

        var criteriaAppearanceTraits =
            criteria.AppearanceTraits;

        if (criteriaAppearanceTraits is not null)
        {
            var height =
                criteriaAppearanceTraits
                    .Height?
                    .FilterRangeValue;

            if (height is not null)
            {
                queryable =
                    queryable
                        .Where(
                            entity =>
                                entity.ProfileTalent!.Profile!.AppearanceTraits!.Height >= height.Min
                                && entity.ProfileTalent.Profile.AppearanceTraits.Height <= height.Max
                        );
            }

            var shoeSize =
                criteriaAppearanceTraits
                    .ShoeSize?
                    .FilterRangeValue;

            if (shoeSize is not null)
            {
                queryable =
                    queryable
                        .Where(
                            entity =>
                                entity.ProfileTalent!.Profile!.AppearanceTraits!.ShoeSize >= shoeSize.Min
                                && entity.ProfileTalent.Profile.AppearanceTraits.ShoeSize <= shoeSize.Max
                        );
            }

            var hairColorType =
                criteriaAppearanceTraits.HairColorType;

            if (hairColorType is not null)
            {
                queryable =
                    queryable
                        .Where(
                            entity =>
                                entity.ProfileTalent!.Profile!.AppearanceTraits!.HairColorType == hairColorType
                        );
            }

            var sexType =
                criteriaAppearanceTraits.SexType;

            if (sexType is not null)
            {
                queryable =
                    queryable
                        .Where(
                            entity =>
                                entity.ProfileTalent!.Profile!.AppearanceTraits!.SexType == sexType
                        );
            }
        }

        var criteriaTagList =
            criteria.TagCollection;

        if (criteriaTagList.IsNotEmpty())
        {
            queryable =
                queryable
                    .Where(
                        entity =>
                            criteriaTagList
                                .All(
                                    filterTag =>
                                        entity
                                            .Portfolio!
                                            .PortfolioTagCollection
                                            .Any(
                                                portfolioTag =>
                                                    portfolioTag.Id == filterTag.Id
                                            )
                                )

                    );
        }

        // Apply dimension filter if DimensionCollection is not empty
        // If dimensions are selected, automatically filter for Models (not Photographers)
        if (criteria.DimensionCollection.IsNotEmpty())
        {
            // Automatically filter for Models when appearance dimensions are selected
            queryable =
                queryable
                    .Where(
                        entity =>
                            entity.TalentType == TalentType.Model
                    );

            var filterCriteriaDimensionList =
                criteria
                    .DimensionCollection
                    .Select(
                        entity => entity.DimensionValueId
                    )
                    .Distinct()
                    .ToList();

            var profileFilterDimensionCollection =
                genericReadRepository.GetCollection<AppearanceTraitsDimensionValue>();

            var profileFilterDimensionQuery =
                profileFilterDimensionCollection
                    .Where(
                        entity =>
                            filterCriteriaDimensionList
                                .Any(
                                    dimensionValueId =>
                                        dimensionValueId == entity.DimensionValueId
                                )
                    )
                    .GroupBy(
                        entity => entity.ProfileId
                    )
                    .Where(
                        entity =>
                            entity.Count() == filterCriteriaDimensionList.Count
                    )
                    .Select(
                        entity => entity.Key
                    );

            queryable =
                queryable
                    .Where(
                        entity =>
                            profileFilterDimensionQuery
                                .Contains(
                                    entity
                                        .ProfileTalent!
                                        .ProfileId
                                )
                    );
        }

        queryable =
            queryable
                .Where(
                    entity =>
                        entity.ProfileTalent != null
                )
                .Distinct()
                .OrderBy(
                    entity => entity.Id
                );

        var filterCursorCacheArgs =
            new FilterCursorCacheArgs(
                filterId,
                filter.Version
            );

        // Try to read cursor from Redis, but don't fail if Redis is unavailable
        Guid? cursorPoint = null;
        try
        {
            var cursor =
                await
                    filterCursorCache
                        .ReadAsync(
                            filterCursorCacheArgs
                        );

            cursorPoint =
                cursor.IsSuccess
                    ? cursor.Value.TalentId
                    : null;
        }
        catch (Exception)
        {
            // Redis unavailable, proceed without cursor (start from beginning)
            cursorPoint = null;
        }

        if (cursorPoint is not null)
        {
            queryable =
                queryable
                    .Where(
                        entity =>
                            entity.ProfileTalent != null && entity.ProfileTalent.ProfileId > cursorPoint
                    );
        }

        // Step 1: Get talent/profile data from DB (without media) - limited by pagination
        var talentListGrouped =
            await queryable
                .Take(limit) // Apply pagination limit
                .Select(
                    entity => new
                    {
                        ProfileId = entity.ProfileTalent != null ? entity.ProfileTalent.ProfileId : Guid.Empty,
                        TalentId = entity.Id,
                        ProfileName = entity.ProfileTalent != null && entity.ProfileTalent.Profile != null ? entity.ProfileTalent.Profile.Name : string.Empty,
                        SexType = entity.ProfileTalent != null && entity.ProfileTalent.Profile != null && entity.ProfileTalent.Profile.AppearanceTraits != null ? entity.ProfileTalent.Profile.AppearanceTraits.SexType : SexType.Undefined,
                        Height = entity.ProfileTalent != null && entity.ProfileTalent.Profile != null && entity.ProfileTalent.Profile.AppearanceTraits != null ? entity.ProfileTalent.Profile.AppearanceTraits.Height : 0,
                        HairColorType = entity.ProfileTalent != null && entity.ProfileTalent.Profile != null && entity.ProfileTalent.Profile.AppearanceTraits != null ? entity.ProfileTalent.Profile.AppearanceTraits.HairColorType : HairColorType.Undefined,
                        EyeColorType = entity.ProfileTalent != null && entity.ProfileTalent.Profile != null && entity.ProfileTalent.Profile.AppearanceTraits != null ? entity.ProfileTalent.Profile.AppearanceTraits.EyeColorType : EyeColorType.Undefined,
                    }
                )
                .ToListAsync();

        // Step 2: Get first portfolio photo for each talent using PortfolioMediaAggregate with PositionIndex
        // With fallback to MediaFile if portfolio is empty
        var talentIds = talentListGrouped
            .Select(t => t.TalentId)
            .ToList();

        var profileIds = talentListGrouped
            .Select(t => t.ProfileId)
            .Distinct()
            .ToList();

        var portfolioMediaCollection = genericReadRepository.GetCollection<PortfolioMediaAggregate>();

        // Get portfolio media with lowest PositionIndex for each talent (newest photo first)
        var portfolioMedia = await portfolioMediaCollection
            .Include(pm => pm.MediaAggregate)
                .ThenInclude(ma => ma!.PreviewMedia)
                    .ThenInclude(m => m!.OriginalFile)
                        .ThenInclude(f => f!.FileResource)
            .Include(pm => pm.Portfolio)
            .Where(pm =>
                pm.Portfolio != null &&
                talentIds.Contains(pm.Portfolio.TalentId))
            .ToListAsync();

        var talentPhotoMap = portfolioMedia
            .GroupBy(pm => pm.Portfolio!.TalentId)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(pm => pm.PositionIndex)
                      .FirstOrDefault()?
                      .MediaAggregate?
                      .PreviewMedia?
                      .OriginalFile?
                      .FileResource?
                      .RelativePath ?? string.Empty
            );

        // Fallback: Get MediaFile for talents without portfolio photos
        var talentsWithoutPhotos = talentIds.Where(tid => !talentPhotoMap.ContainsKey(tid) || string.IsNullOrEmpty(talentPhotoMap[tid])).ToList();

        if (talentsWithoutPhotos.Any())
        {
            var mediaFileCollection = genericReadRepository.GetCollection<MediaFile>();
            var fallbackMedia = await mediaFileCollection
                .Include(mf => mf.FileResource)
                .Where(mf => profileIds.Contains(mf.ProfileId))
                .OrderByDescending(mf => mf.Id)
                .ToListAsync();

            var profileMediaMap = fallbackMedia
                .GroupBy(mf => mf.ProfileId)
                .ToDictionary(g => g.Key, g => g.First().FileResource?.RelativePath ?? string.Empty);

            // Map ProfileId to TalentId for fallback
            var profileToTalentMap = talentListGrouped
                .Where(t => talentsWithoutPhotos.Contains(t.TalentId))
                .GroupBy(t => t.ProfileId)
                .ToDictionary(g => g.Key, g => g.First().TalentId);

            foreach (var kvp in profileToTalentMap)
            {
                if (profileMediaMap.TryGetValue(kvp.Key, out var path))
                {
                    talentPhotoMap[kvp.Value] = path;
                }
            }
        }

        // Step 3: Combine talent data with media paths
        var talentListWithMedia = talentListGrouped
            .Select(t => new
            {
                t.ProfileId,
                t.TalentId,
                t.ProfileName,
                t.SexType,
                t.Height,
                t.HairColorType,
                t.EyeColorType,
                FirstMediaFilePath = talentPhotoMap.TryGetValue(t.TalentId, out var path) ? path : null
            })
            .ToList();

        var talentFilterDimensionList =
            new List<UserFilterResultListItemResult>();

        foreach (var model in talentListWithMedia)
        {
            var listItem =
                new UserFilterResultListItemResult(
                    model.ProfileId,
                    model.TalentId,
                    model.ProfileName,
                    model.FirstMediaFilePath ?? string.Empty,
                    model.SexType.ToString(),
                    model.Height > 0 ? model.Height : null,
                    model.HairColorType.ToString(),
                    model.EyeColorType.ToString()
                );

            talentFilterDimensionList
                .Add(
                    listItem
                );
        }

        var cursorLastProfileId =
            talentFilterDimensionList
                .LastOrDefault()?
                .ProfileId;

        if (cursorLastProfileId is null || cursorLastProfileId == Guid.Empty)
        {
            try
            {
                await
                    filterCursorCache
                        .DeleteAsync(
                            filterCursorCacheArgs
                        );
            }
            catch (Exception)
            {
                // Redis unavailable, ignore
            }

            var emptyListResult =
                new UserFilterResultListResult(
                    []
                );

            return
                emptyListResult;
        }

        var filterCursorCacheModel =
            new FilterCursorCacheModel(
                cursorLastProfileId.Value,
                filterId,
                filter.Version
            );

        try
        {
            await
                filterCursorCache
                    .SetAsync(
                        filterCursorCacheArgs,
                        filterCursorCacheModel
                    );
        }
        catch (Exception)
        {
            // Redis unavailable, ignore
        }

        var result =
            new UserFilterResultListResult(
                talentFilterDimensionList
            );

        return
            result;
    }

    private async Task<Filter?> GetFilter(
        Guid filterId,
        Guid userId
    )
    {
        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.AppearanceTraits
                    )
                    .ThenInclude(
                        entity => entity.Height
                    )
                    .ThenInclude(
                        entity => entity.FilterRangeValue
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.AppearanceTraits
                    )
                    .ThenInclude(
                        entity => entity.ShoeSize
                    )
                    .ThenInclude(
                        entity => entity.FilterRangeValue
                    )

                    .Include(
                        entity => entity.FilterCriteria
                    )
                    .ThenInclude(
                        entity => entity.Location
                    )
                    .ThenInclude(
                        entity => entity.Place
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
                        entity => entity.DimensionCollection
                    )

                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == filterId
                            && entity.ApplicationUserId == userId
                    );
        return filter;
    }
}