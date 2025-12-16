using System;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Talents;
using FashionFace.Facades.Users.Interfaces.Talents;
using FashionFace.Facades.Users.Models.Talents;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Context.Models.Talents;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Talents;

public sealed class UserTalentCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserTalentCreateFacade
{
    public async Task<UserTalentCreateResult> Execute(
        UserTalentCreateArgs args
    )
    {
        var (
            userId,
            talentType,
            talentDescription,
            portfolioDescription
            ) = args;

        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        var profile =
            await
                profileCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                    );

        if (profile is null)
        {
            throw exceptionDescriptor.NotFound<Profile>();
        }

        var profileTalentCollection =
            genericReadRepository.GetCollection<ProfileTalent>();

        var lastProfileTalent =
            await
                profileTalentCollection
                    .Where(
                        entity =>
                            entity.ProfileId == profile.Id
                    )
                    .OrderBy(
                        entity =>
                            entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            lastProfileTalent?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var talentId =
            Guid.NewGuid();

        var portfolioId =
            Guid.NewGuid();

        var portfolio =
            new Portfolio
            {
                Id = portfolioId,
                IsDeleted = false,
                TalentId = talentId,
                Description = portfolioDescription,
            };

        var talent =
            new Talent
            {
                Id = talentId,
                IsDeleted = false,
                TalentType = talentType,
                Description = talentDescription,
                Portfolio = portfolio,
            };

        var profileTalent =
            new ProfileTalent
            {
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
                TalentId = talentId,
                Talent = talent,
                PositionIndex = positionIndex,
            };

        await
            createRepository
                .CreateAsync(
                    profileTalent
                );

        var result =
            new UserTalentCreateResult(
                talent.Id
            );

        return
            result;
    }
}