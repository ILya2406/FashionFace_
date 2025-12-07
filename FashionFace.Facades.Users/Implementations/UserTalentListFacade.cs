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

        var talentCollection =
            genericReadRepository.GetCollection<Talent>();

        var talentList =
            await
                talentCollection
                    .Where(
                        entity =>
                            entity.ProfileId == profileId
                    )
                    .ToListAsync();

        var talentListItemResultList =
            talentList
                .Select(
                    entity =>
                        new UserTalentListItemResult(
                            entity.Id,
                            entity.Description,
                            entity.Type
                        )
                )
                .ToList();

        var result =
            new ListResult<UserTalentListItemResult>(
                talentList.Count,
                talentListItemResultList
            );

        return
            result;
    }
}