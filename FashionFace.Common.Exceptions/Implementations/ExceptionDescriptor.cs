using System.Collections.Generic;
using System.Linq;

using FashionFace.Common.Exceptions.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Exceptions.Model;

using Microsoft.AspNetCore.Identity;

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

    public BusinessLogicException IdentityErrorList(
        IEnumerable<IdentityError> identityErrorList
    )
    {
        var identityErrorCodeList =
            identityErrorList
                .Select(
                    error => $"{error.Code}"
                )
                .ToList();

        var data =
            new Dictionary<string, object>
            {
                { "error", identityErrorCodeList },
            };

        var businessLogicException =
            new BusinessLogicException(
                ExceptionConstants.IdentityErrorList,
                data
            );

        return
            businessLogicException;
    }
}