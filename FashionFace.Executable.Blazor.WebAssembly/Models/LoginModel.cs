namespace FashionFace.Executable.Blazor.WebAssembly.Models;

public class LoginModel
{
    private string username = string.Empty;
    private string password = string.Empty;

    public string Username
    {
        get => username;
        set => username = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }
}