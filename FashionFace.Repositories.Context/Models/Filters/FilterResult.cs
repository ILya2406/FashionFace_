using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterResult : EntityBase
{
    public required Guid FilterId { get; set; }

    public required FilterResultStatus FilterResultStatus { get; set; }

    public Filter? Filter { get; set; }

    public ICollection<FilterResultTalent> FilterResultTalentCollection { get; set; }
}