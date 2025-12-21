namespace FashionFace.Controllers.Users.Responses.Models.Filters
{
    public sealed record FilterRangeResponse(
        int? Min,
        int? Max
    );
}