namespace FashionFace.Controllers.Users.Responses.Models.Locations;

public sealed record UserPlaceResponse(
    string Street,
    UserBuildingResponse? Building,
    UserLandmarkResponse? Landmark
);