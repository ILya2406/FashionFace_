using System.Threading.Tasks;

using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Repositories.Context.Models.IdentityEntities;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Identity.Implementations;

public sealed class PasswordManagerDecorator(
    UserManager<ApplicationUser> userManager
) : IPasswordManagerDecorator
{
    public async Task<IdentityResult> ResetPasswordAsync(
        ApplicationUser user,
        string token,
        string newPassword
    ) => await userManager.ResetPasswordAsync(
        user,
        token,
        newPassword
    );

    public async Task<IdentityResult> ChangePasswordAsync(
        ApplicationUser user,
        string currentPassword,
        string newPassword
    ) => await userManager.ChangePasswordAsync(
        user,
        currentPassword,
        newPassword
    );

    public async Task<bool> CheckPasswordAsync(
        ApplicationUser user,
        string password
    ) => await userManager.CheckPasswordAsync(
        user,
        password
    );
}