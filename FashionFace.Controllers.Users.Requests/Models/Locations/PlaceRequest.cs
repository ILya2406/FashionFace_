namespace FashionFace.Controllers.Users.Requests.Models.Locations;

public sealed record PlaceRequest(
    string Street,
    string? BuildingName,
    string? LandmarkName
);