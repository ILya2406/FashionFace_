namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserPlaceResponse(
    string Street,
    UserBuildingResponse? Building,
    UserLandmarkResponse? Landmark
);