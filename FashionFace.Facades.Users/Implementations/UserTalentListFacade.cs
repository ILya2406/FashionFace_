using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

public sealed class UserTalentListFacade(
    IGenericReadRepository genericReadRepository
) : IUserTalentListFacade
{
    public async Task<ListResult<UserTalentListItemResult>> Execute(
        UserTalentListArgs args
    )
    {
        var (
            _,
            profileId
            ) = args;

        var profileTalentCollection =
            genericReadRepository.GetCollection<ProfileTalent>();

        var profileTalentList =
            await
                profileTalentCollection
                    .Include(
                        entity => entity.Talent
                    )
                    .Where(
                        entity =>
                            entity.ProfileId == profileId
                    )
                    .ToListAsync();

        var talentListItemResultList =
            profileTalentList
                .Select(
                    entity =>
                        new UserTalentListItemResult(
                            entity.Talent!.Id,
                            entity.PositionIndex,
                            entity.Talent.Description,
                            entity.Talent.TalentType
                        )
                )
                .ToList();

        var result =
            new ListResult<UserTalentListItemResult>(
                profileTalentList.Count,
                talentListItemResultList
            );

        return
            result;
    }
}