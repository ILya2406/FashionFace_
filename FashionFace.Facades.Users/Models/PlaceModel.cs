namespace FashionFace.Facades.Users.Models;

public sealed record PlaceModel(
    string Street,
    BuildingModel? Building,
    LandmarkModel? Landmark
);