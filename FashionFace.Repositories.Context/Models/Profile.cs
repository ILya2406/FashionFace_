using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models;

public sealed class Profile : EntityBase
{
    public required Guid ApplicationUserId { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required AgeCategoryType AgeCategoryType { get; set; }

    public AppearanceTraits? AppearanceTraits { get; set; }
    public ICollection<Talent> TalentCollection { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
}