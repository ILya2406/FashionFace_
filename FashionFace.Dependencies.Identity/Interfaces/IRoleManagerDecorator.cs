using System.Threading.Tasks;

using FashionFace.Repositories.Context.Models.IdentityEntities;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Identity.Interfaces;

public interface IRoleManagerDecorator
{
    Task<IdentityResult> AddToRoleAsync(
        ApplicationUser user,
        string role
    );

    Task<string> GetRoleAsync(
        ApplicationUser user
    );
}