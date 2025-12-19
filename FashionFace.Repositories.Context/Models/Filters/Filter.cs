using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class Filter : EntityBase,
    IWithIsDeleted,
    IWithPositionIndex
{
    public required Guid ApplicationUserId { get; set; }
    public required Guid FilterCriteriaId {get; set;}

    public required bool IsDeleted { get; set; }
    public required double PositionIndex { get; set; }
    public required int Version { get; set; }
    public required string Name { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public FilterCriteria? FilterCriteria { get; set; }
}