using System.Diagnostics.CodeAnalysis;

using FashionFace.Services.Singleton.Models;

namespace FashionFace.Executable.Blazor.WebAssembly.Models;

public sealed record ApiResultContainer<T>
{
    private ApiResultContainer(
        ErrorsContainerModel error
    )
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    private ApiResultContainer(
        bool isSuccess,
        T value
    )
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = null;
    }

    [MemberNotNullWhen(
        true,
        nameof(Value)
    )]
    public bool IsSuccess { get; }

    public T? Value { get; }

    public ErrorsContainerModel? Error { get; }

    public static ApiResultContainer<T> Failed(
        ErrorsContainerModel errorDetails
    ) =>
        new(
            errorDetails
        );

    public static ApiResultContainer<T> Successful(
        T value
    ) =>
        new(
            true,
            value
        );
}