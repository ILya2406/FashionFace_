using System;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Filters;

[UserControllerGroup(
    "Filter"
)]
[Route(
    "api/v1/user/filter"
)]
public sealed class UserFilterUpdateController(
    IUserFilterUpdateFacade facade
) : BaseUserController
{
    [HttpPut]
    public async Task Invoke(
        [FromBody] UserFilterUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            GetUserFilterUpdateArgs(
                userId,
                request
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }

    private static UserFilterUpdateArgs GetUserFilterUpdateArgs(
        Guid userId,
        UserFilterUpdateRequest request
    )
    {
        var requestFilterLocation =
            request.FilterLocation;

        var filterLocationArgs =
            requestFilterLocation is null
                ? null
                : new FilterLocationArgs(
                    requestFilterLocation.CityId,
                    requestFilterLocation.LocationType
                );

        var requestFilterAppearanceTraits =
            request.FilterAppearanceTraits;

        FilterAppearanceTraitsArgs? filterAppearanceTraitsArgs = null;

        if (requestFilterAppearanceTraits is not null)
        {
            var filterMaleTraitsRequest =
                requestFilterAppearanceTraits.FilterMaleTraits;

            var filterMaleTraitsArgs =
                filterMaleTraitsRequest is null
                    ? null
                    : new FilterMaleTraitsArgs(
                        filterMaleTraitsRequest.FacialHairLengthType
                    );

            var filterFemaleTraitsRequest =
                requestFilterAppearanceTraits.FilterFemaleTraits;

            var filterFemaleTraitsArgs =
                filterFemaleTraitsRequest is null
                    ? null
                    : new FilterFemaleTraitsArgs(
                        filterFemaleTraitsRequest.BustSizeType
                    );

            filterAppearanceTraitsArgs =
                new(
                    requestFilterAppearanceTraits.SexType,
                    requestFilterAppearanceTraits.FaceType,
                    requestFilterAppearanceTraits.HairColorType,
                    requestFilterAppearanceTraits.HairType,
                    requestFilterAppearanceTraits.HairLengthType,
                    requestFilterAppearanceTraits.BodyType,
                    requestFilterAppearanceTraits.SkinToneType,
                    requestFilterAppearanceTraits.EyeShapeType,
                    requestFilterAppearanceTraits.EyeColorType,
                    requestFilterAppearanceTraits.NoseType,
                    requestFilterAppearanceTraits.JawType,
                    requestFilterAppearanceTraits.Height,
                    requestFilterAppearanceTraits.ShoeSize,
                    filterMaleTraitsArgs,
                    filterFemaleTraitsArgs
                );
        }

        var facadeArgs =
            new UserFilterUpdateArgs(
                userId,
                request.FilterId,
                request.Name,
                request.PositionIndex,
                request.TalentType,
                filterLocationArgs,
                filterAppearanceTraitsArgs
            );

        return
            facadeArgs;
    }
}