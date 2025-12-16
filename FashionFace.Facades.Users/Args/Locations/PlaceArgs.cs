namespace FashionFace.Facades.Users.Args.Locations;

public sealed record PlaceArgs(
    string Street,
    string? BuildingName,
    string? LandmarkName
);