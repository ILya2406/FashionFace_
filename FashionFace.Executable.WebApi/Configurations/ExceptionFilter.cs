using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Model;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.WebApi.Configurations;

public sealed class ExceptionFilter(
    IErrorResponseService errorResponseService,
    ILogger<ExceptionFilter> logger
) : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(
        ExceptionContext context
    )
    {
        var traceId =
            Guid
                .NewGuid()
                .ToString();

        var exception =
            context.Exception;

        var result =
            errorResponseService
                .CreateErrorResponse(
                    exception,
                    traceId
                );

        if (exception is BusinessLogicException businessLogicException)
        {
            logger
                .LogError(
                    exception,
                    "Caught BusinessLogicException.\nCode: {@code}\nData: {@data}",
                    businessLogicException.Code,
                    ((Exception)businessLogicException).Data
                );
        }
        else
        {
            logger
                .LogError(
                    exception,
                    exception.Message
                );
        }

        var contextResult =
            new ObjectResult(
                result.Error
            )
            {
                StatusCode =
                    result.StatusCode,
            };

        context.Result =
            contextResult;
    }
}