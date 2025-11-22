using System.Text.Json;

namespace FashionFace.Common.Extensions.Implementations;

public sealed class PascalCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(
        string name
    )
    {
        var isNullOrEmpty =
            string
                .IsNullOrEmpty(
                    name
                );

        if (isNullOrEmpty)
        {
            return name;
        }

        var firstLetter =
            char
                .ToUpper(
                    name[0]
                );

        var substring =
            name[1..];

        var newName =
            firstLetter
            + substring;

        return
            newName;
    }
}