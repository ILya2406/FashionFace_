using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterRangeValue : EntityBase
{
    public required int? Min { get; set; }
    public required int? Max { get; set; }
}