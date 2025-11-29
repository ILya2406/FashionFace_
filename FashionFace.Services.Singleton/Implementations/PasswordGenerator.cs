using System;
using System.Security.Cryptography;
using System.Text;

using FashionFace.Services.Singleton.Interfaces;

using static FashionFace.Common.Constants.Constants.RandomPasswordConstants;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class PasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword(
        int length = PasswordLength
    )
    {
        var stringBuilder =
            new StringBuilder(length);

        var alphabetLength =
            Alphabet.Length;

        using var randomNumberGenerator =
            RandomNumberGenerator.Create();

        var buffer =
            new byte[4];

        for (var i = 0; i < length; i++)
        {
            randomNumberGenerator
                .GetBytes(
                    buffer
                );

            var randomIntegerValue =
                BitConverter
                    .ToInt32(
                        buffer,
                        0
                    );

            var index =
                randomIntegerValue % alphabetLength;

            var charValue =
                Alphabet[index];

            stringBuilder
                .Append(
                    charValue
                );
        }

        var password =
            stringBuilder.ToString();

        return
            password;
    }
}