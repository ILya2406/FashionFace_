using FashionFace.Repositories.Context.Models;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Identity.Interfaces;

public interface IPasswordManagerDecorator
{
    Task<IdentityResult> ResetPasswordAsync(
        ApplicationUser user,
        string token,
        string newPassword
    );

    Task<IdentityResult> ChangePasswordAsync(
        ApplicationUser user,
        string currentPassword,
        string newPassword
    );
}