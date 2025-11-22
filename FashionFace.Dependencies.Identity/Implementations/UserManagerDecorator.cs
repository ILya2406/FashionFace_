using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Repositories.Context.Models;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Identity.Implementations;

public sealed class UserManagerDecorator(
    UserManager<ApplicationUser> userManager
) : IUserManagerDecorator
{
    public async Task<ApplicationUser?> FindByIdAsync(Guid id) =>
        await userManager.FindByIdAsync(
            id.ToString()
        );

    public async Task<ApplicationUser?> FindByEmailAsync(string username) =>
        await userManager.FindByEmailAsync(
            username
        );

    public async Task<ApplicationUser?> FindByNameAsync(string username) =>
        await userManager.FindByNameAsync(
            username
        );

    public async Task<bool> CheckPasswordAsync(
        ApplicationUser user,
        string password
    ) => await userManager.CheckPasswordAsync(
        user,
        password
    );

    public async Task<IdentityResult> CreateAsync(
        ApplicationUser user,
        string password
    ) => await userManager.CreateAsync(
        user,
        password
    );

    public async Task<IdentityResult> ChangePasswordAsync(
        ApplicationUser user,
        string oldPassword,
        string newPassword
    ) => await userManager.ChangePasswordAsync(
        user,
        oldPassword,
        newPassword
    );
}