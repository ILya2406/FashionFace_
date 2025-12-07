using Microsoft.AspNetCore.Identity;

namespace FashionFace.Dependencies.Crypt.Implementations;

public sealed class BcryptPasswordHasher<TUser> :
    IPasswordHasher<TUser>
    where TUser : class
{
    public string HashPassword(
        TUser user,
        string password
    ) =>
        BCrypt.Net.BCrypt.HashPassword(
            password,
            12
        )!;

    public PasswordVerificationResult VerifyHashedPassword(
        TUser user,
        string hashedPassword,
        string providedPassword
    )
    {
        var isValid =
            BCrypt.Net.BCrypt.Verify(
                providedPassword,
                hashedPassword
            );

        var result =
            isValid
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;

        return
            result;
    }
}