using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.UserToUserChats;
using FashionFace.Controllers.Users.Responses.Models.UserToUserChats;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Facades.Users.Models.UserToUserChats;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.UserToUserChats;

[UserControllerGroup(
    "UserToUserChat"
)]
[Route(
    "api/v1/user-to-user-chat/list"
)]
public sealed class UserToUserChatListController(
    IUserToUserChatListFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<ListResponse<UserToUserChatListItemResponse>> Invoke(
        [FromQuery] UserToUserChatListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserToUserChatListArgs(
                userId,
                request.Offset,
                request.Limit
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            GetResponse(
                result
            );

        return
            response;
    }

    private static ListResponse<UserToUserChatListItemResponse> GetResponse(
        ListResult<UserToUserChatListItemResult> result
    )
    {
        var responseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserToUserChatListItemResponse(
                            entity.ChatId,
                            entity
                                .UserList
                                .Select(
                                    user =>
                                        new ChatUserInfoResponse(
                                            user.UserId,
                                            user.Name
                                        )
                                )
                                .ToList(),
                            entity.UnreadCount
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserToUserChatListItemResponse>(
                result.TotalCount,
                responseList
            );

        return
            response;
    }
}