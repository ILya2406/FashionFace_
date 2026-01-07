using System;
using System.Text;

namespace FashionFace.Common.Extensions.Implementations;

public static class TokenDecoderExtensions
{
    public static string DecodeUsernameToken(this string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be null or empty", nameof(token));
        }
        try
        {
            string text = token.Replace('-', '+').Replace('_', '/');
            int num = 4 - text.Length % 4;
            if (num < 4)
            {
                text += new string('=', num);
            }
            byte[] array = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(array);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid token format: " + ex.Message, nameof(token), ex);
        }
    }

    public static string EncodeUsernameToken(this string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        }
        string text = username.StartsWith("@") ? username.Substring(1) : username;
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(text))
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
