using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Exceptions.Model;

namespace FashionFace.Common.Exceptions.Implementations;

public sealed class ExceptionDescriptor : IExceptionDescriptor
{
    public BusinessLogicException Exception(
        string code,
        IDictionary<string, object>? data = null
    ) =>
        new(
            code,
            data ?? new Dictionary<string, object>()
        );

    public BusinessLogicException Unauthorized(IDictionary<string, object>? data = null) =>
        new(
            "Unauthorized",
            data ?? new Dictionary<string, object>()
        );

    public BusinessLogicException NotFound<TEntity>(IDictionary<string, object>? data = null) =>
        new(
            "NotFound",
            data
            ?? new Dictionary<string, object>
            {
                { "Type", $"{typeof(TEntity)}" },
            }
        );

    public BusinessLogicException Exists<TEntity>(IDictionary<string, object>? data = null) =>
        new(
            "Exist",
            data
            ?? new Dictionary<string, object>
            {
                { "Type", $"{typeof(TEntity)}" },
            }
        );
}