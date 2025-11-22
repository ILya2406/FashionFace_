using FashionFace.Repositories.Context.Models;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Identity.Interfaces;

public interface IUserManagerDecorator
{
    Task<ApplicationUser?> FindByIdAsync(Guid id);
    Task<ApplicationUser?> FindByEmailAsync(string username);
    Task<ApplicationUser?> FindByNameAsync(string username);

    Task<bool> CheckPasswordAsync(
        ApplicationUser user,
        string password
    );

    Task<IdentityResult> CreateAsync(
        ApplicationUser user,
        string password
    );

    Task<IdentityResult> ChangePasswordAsync(
        ApplicationUser user,
        string oldPassword,
        string newPassword
    );
}