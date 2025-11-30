using System.Collections.Generic;

using FashionFace.Common.Exceptions.Model;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Common.Exceptions.Interfaces;

public interface IExceptionDescriptor
{
    BusinessLogicException Exception(
        string code,
        IDictionary<string, object>? data = null
    );

    BusinessLogicException Unauthorized(IDictionary<string, object>? data = null);
    BusinessLogicException NotFound<TEntity>(IDictionary<string, object>? data = null);
    BusinessLogicException Exists<TEntity>(IDictionary<string, object>? data = null);
    BusinessLogicException IdentityErrorList(IEnumerable<IdentityError> identityErrorList);
}