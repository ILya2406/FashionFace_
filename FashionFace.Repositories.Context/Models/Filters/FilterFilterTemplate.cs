using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Talents;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterFilterTemplate : EntityBase
{
    public required Guid FilterId { get; set; }
    public required Guid FilterTemplateId { get; set; }

    public Filter? Filter { get; set; }
    public FilterTemplate? FilterTemplate { get; set; }
}