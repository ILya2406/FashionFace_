using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class Filter : EntityBase,
    IWithIsDeleted,
    IWithPositionIndex
{
    public required Guid ApplicationUserId { get; set; }

    public required bool IsDeleted { get; set; }
    public required int Version { get; set; }
    public required double PositionIndex { get; set; }
    public required string Name { get; set; }
    public TalentType? TalentType { get; set; }

    public FilterLocation? FilterLocation { get; set; }
    public FilterAppearanceTraits? FilterAppearanceTraits { get; set; }

    public ICollection<FilterTag> FilterTagCollection { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public FilterResult? FilterResult { get; set; }
}