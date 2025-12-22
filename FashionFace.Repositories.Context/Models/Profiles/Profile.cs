using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.Profiles;

public sealed class Profile : EntityBase,
    IWithIsDeleted
{
    public required Guid ApplicationUserId { get; set; }
    public required bool IsDeleted { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required AgeCategoryType AgeCategoryType { get; set; }

    public Dossier? Dossier { get; set; }
    public AppearanceTraits? AppearanceTraits { get; set; }
    public ICollection<ProfileTalent> ProfileTalentCollection { get; set; }
    public ICollection<MediaFile> MediaFileCollection { get; set; }
    public ICollection<AppearanceTraitsDimensionValue> AppearanceTraitsDimensionValueCollection { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public ProfileMediaAggregate? ProfileMediaAggregate { get; set; }
}