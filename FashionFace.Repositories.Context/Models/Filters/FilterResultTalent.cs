using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Talents;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterResultTalent : EntityBase, IWithPositionIndex
{
    public required Guid FilterResultId { get; set; }
    public required Guid TalentId { get; set; }

    public required double PositionIndex { get; set; }
    public required bool IsValidated { get; set; }

    public FilterResult? FilterResult { get; set; }
    public Talent? Talent { get; set; }
}