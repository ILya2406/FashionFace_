using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models;

public sealed class Profile : EntityBase
{
    public required Guid ApplicationUserId { get; set; }

    public string? GivenName { get; set; }
    public string? MiddleName { get; set; }
    public string? FamilyName { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public ICollection<Talent> TalentCollection { get; set; }
}