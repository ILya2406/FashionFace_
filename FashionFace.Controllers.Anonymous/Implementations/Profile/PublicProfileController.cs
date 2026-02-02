using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Controllers.Anonymous.Implementations.Base;
using FashionFace.Controllers.Anonymous.Responses.Models.Profile;
using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Context.Models.Tags;
using FashionFace.Repositories.Context.Models.Talents;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionFace.Controllers.Anonymous.Implementations.Profile;

[ProfileControllerGroup]
[Route("api/anonymous/profile")]
public sealed class PublicProfileController(
    ApplicationDatabaseContext context
) : AnonymousControllerBase
{
    [HttpGet("{token}")]
    public async Task<ActionResult<PublicProfileResponse>> GetPublicProfile(
        [FromRoute] string token
    )
    {
        Repositories.Context.Models.Profiles.Profile? profile = null;

        // First, try to parse as GUID (profileId)
        if (Guid.TryParse(token, out var profileId))
        {
            profile = await context
                .Set<Repositories.Context.Models.Profiles.Profile>()
                .Include(p => p.ApplicationUser)
                .Include(p => p.AppearanceTraits)
                .Include(p => p.MediaFileCollection)
                    .ThenInclude(mf => mf.FileResource)
                .Include(p => p.ProfileTalentCollection)
                    .ThenInclude(pt => pt.Talent)
                    .ThenInclude(t => t.Portfolio)
                    .ThenInclude(port => port.PortfolioMediaCollection)
                    .ThenInclude(pm => pm.MediaAggregate)
                    .ThenInclude(ma => ma.PreviewMedia)
                    .ThenInclude(m => m.OptimizedFile)
                        .ThenInclude(of => of.FileResource)
                .Include(p => p.ProfileTalentCollection)
                    .ThenInclude(pt => pt.Talent)
                    .ThenInclude(t => t.Portfolio)
                    .ThenInclude(port => port.PortfolioTagCollection)
                    .ThenInclude(ptag => ptag.Tag)
                .FirstOrDefaultAsync(p => p.Id == profileId);
        }

        // If not found by GUID, try to decode as username token
        if (profile == null)
        {
            string username;
            try
            {
                username = token.DecodeUsernameToken();
            }
            catch (ArgumentException)
            {
                return BadRequest(new { error = "Invalid token format" });
            }

            // Normalize the search term (replace underscores with spaces for name matching)
            var searchName = username.Replace("_", " ");

            profile = await context
                .Set<Repositories.Context.Models.Profiles.Profile>()
                .Include(p => p.ApplicationUser)
                .Include(p => p.AppearanceTraits)
                .Include(p => p.MediaFileCollection)
                    .ThenInclude(mf => mf.FileResource)
                .Include(p => p.ProfileTalentCollection)
                    .ThenInclude(pt => pt.Talent)
                    .ThenInclude(t => t.Portfolio)
                    .ThenInclude(port => port.PortfolioMediaCollection)
                    .ThenInclude(pm => pm.MediaAggregate)
                    .ThenInclude(ma => ma.PreviewMedia)
                    .ThenInclude(m => m.OptimizedFile)
                        .ThenInclude(of => of.FileResource)
                .Include(p => p.ProfileTalentCollection)
                    .ThenInclude(pt => pt.Talent)
                    .ThenInclude(t => t.Portfolio)
                    .ThenInclude(port => port.PortfolioTagCollection)
                    .ThenInclude(ptag => ptag.Tag)
                .FirstOrDefaultAsync(p =>
                    p.ApplicationUser != null &&
                    (p.ApplicationUser.UserName == username ||
                     p.ApplicationUser.UserName == $"@{username}" ||
                     p.Name == username ||
                     p.Name == searchName)
                );
        }

        if (profile == null)
        {
            return NotFound(new { error = "Profile not found" });
        }

        var applicationUser = profile.ApplicationUser!;
        var talent = profile.ProfileTalentCollection?.FirstOrDefault()?.Talent;
        var portfolio = talent?.Portfolio;
        var type = talent?.TalentType.ToString()?.ToLower() ?? "model";

        var portfolioMedia = portfolio?.PortfolioMediaCollection?
            .OrderBy(pm => pm.PositionIndex)
            .ToList() ?? new List<PortfolioMediaAggregate>();

        var coverUrl = portfolioMedia.FirstOrDefault()?.MediaAggregate?.PreviewMedia?.OptimizedFile?.FileResource?.RelativePath;

        var mediaUrls = portfolioMedia
            .Select(pm => pm.MediaAggregate?.PreviewMedia?.OptimizedFile?.FileResource?.RelativePath)
            .Where(url => url != null)
            .Cast<string>()
            .ToList();

        if (!mediaUrls.Any() && profile.MediaFileCollection?.Any() == true)
        {
            mediaUrls = profile.MediaFileCollection
                .Select(m => m.FileResource?.RelativePath)
                .Where(url => url != null)
                .Cast<string>()
                .ToList();
            coverUrl = mediaUrls.FirstOrDefault();
        }

        var tags = portfolio?.PortfolioTagCollection?
            .Select(pt => pt.Tag?.Name)
            .Where(name => name != null)
            .Cast<string>()
            .ToList() ?? new List<string>();

        AppearanceTraitsResponse? appearanceTraits = null;
        if (profile.AppearanceTraits != null)
        {
            var traits = profile.AppearanceTraits;
            appearanceTraits = new AppearanceTraitsResponse(
                traits.Height > 0 ? traits.Height : null,
                traits.ShoeSize > 0 ? traits.ShoeSize : null,
                traits.HairColorType.ToString(),
                traits.HairType.ToString(),
                traits.HairLengthType.ToString(),
                traits.EyeColorType.ToString(),
                traits.EyeShapeType.ToString(),
                traits.SkinToneType.ToString(),
                traits.BodyType.ToString(),
                traits.FaceType.ToString(),
                traits.NoseType.ToString(),
                traits.JawType.ToString()
            );
        }

        var response = new PublicProfileResponse(
            applicationUser.Id,
            profile.Name,
            profile.Description,
            type,
            null, // city
            applicationUser.UserName,
            null, // instagramUsername
            applicationUser.Email,
            null, // portfolioUrl
            FormatMediaUrl(coverUrl),
            profile.AppearanceTraits?.SexType.ToString(),
            profile.AgeCategoryType.ToString(),
            tags.Any() ? tags : null,
            profile.CreatedAt,
            appearanceTraits,
            mediaUrls.Any() ? mediaUrls.Select(FormatMediaUrl).ToList() : null,
            talent?.Id
        );

        return Ok(response);
    }

    private static string? FormatMediaUrl(string? url)
    {
        if (url == null) return null;
        if (url.StartsWith("http://") || url.StartsWith("https://"))
            return url;
        return "/files/" + url;
    }
}
