using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FashionFace.Common.Extensions.Implementations;

public static class StringExtensions
{
    public static bool IsEmpty(
        [NotNullWhen(
            false
        )]
        this string? value
    ) =>
        string
            .IsNullOrWhiteSpace(
                value
            );

    public static bool IsNotEmpty(
        [NotNullWhen(
            true
        )]
        this string? value
    ) =>
        !string
            .IsNullOrWhiteSpace(
                value
            );

    public static bool IsEqualTo(
        this string source,
        string pattern,
        StringComparison compareType =
            StringComparison.InvariantCultureIgnoreCase
    ) =>
        string
            .Equals(
                source,
                pattern,
                compareType
            );

    public static bool IsNotEqualTo(
        this string source,
        string pattern,
        StringComparison compareType =
            StringComparison.InvariantCultureIgnoreCase
    ) =>
        !string
            .Equals(
                source,
                pattern,
                compareType
            );

    public static byte[] GetUtf8Bytes(
        this string message
    ) =>
        Encoding
            .UTF8
            .GetBytes(
                message
            );
}