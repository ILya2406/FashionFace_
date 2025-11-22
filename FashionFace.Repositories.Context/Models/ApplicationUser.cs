using System;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Repositories.Context.Models;

public sealed class ApplicationUser : IdentityUser<Guid> { }