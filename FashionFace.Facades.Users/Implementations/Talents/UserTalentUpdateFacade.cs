using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Talents;
using FashionFace.Facades.Users.Interfaces.Talents;
using FashionFace.Repositories.Context.Models.Talents;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Talents;

public sealed class UserTalentUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository
) : IUserTalentUpdateFacade
{
    public async Task Execute(
        UserTalentUpdateArgs args
    )
    {
        var (
            userId,
            talentId,
            description
            ) = args;

        var talentCollection =
            genericReadRepository.GetCollection<Talent>();

        var talent =
            await
                talentCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == talentId
                            && entity
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId == userId
                    );

        if (talent is null)
        {
            throw exceptionDescriptor.NotFound<Talent>();
        }

        if (description is not null)
        {
            talent.Description =
                description;
        }

        await
            updateRepository
                .UpdateAsync(
                    talent
                );
    }
}