using static FashionFace.Common.Constants.Constants.RandomPasswordConstants;

namespace FashionFace.Services.Singleton.Interfaces;

public interface IPasswordGenerator
{
    string GeneratePassword(
        int length = PasswordLength
    );
}