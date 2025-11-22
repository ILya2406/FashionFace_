using FashionFace.Common.Exceptions.Model;

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
}