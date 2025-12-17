using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraitsEntities;

public sealed class UserMaleTraitsFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserMaleTraitsFacade
{
    public async Task<UserMaleTraitsResult> Execute(
        UserMaleTraitsArgs args
    )
    {
        var (
            _,
            profileId
            ) = args;

        var maleTraitsCollection =
            genericReadRepository.GetCollection<MaleTraits>();

        var maleTraits =
            await
                maleTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.AppearanceTraits!.ProfileId == profileId
                    );

        if (maleTraits is null)
        {
            throw exceptionDescriptor.NotFound<MaleTraits>();
        }

        var result =
            new UserMaleTraitsResult(
                maleTraits.FacialHairLengthType
            );

        return
            result;
    }
}