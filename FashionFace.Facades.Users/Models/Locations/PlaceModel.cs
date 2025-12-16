namespace FashionFace.Facades.Users.Models.Locations;

public sealed record PlaceModel(
    string Street,
    BuildingModel? Building,
    LandmarkModel? Landmark
);