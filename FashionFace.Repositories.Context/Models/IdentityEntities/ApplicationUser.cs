using System;

using FashionFace.Repositories.Context.Models.Profiles;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Repositories.Context.Models.IdentityEntities;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public Profile? Profile { get; set; }
}