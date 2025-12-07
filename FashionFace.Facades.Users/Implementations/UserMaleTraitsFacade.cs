using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

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