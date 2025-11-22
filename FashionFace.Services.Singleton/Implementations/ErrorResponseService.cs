using System;
using System.Collections.Generic;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Model;
using FashionFace.Services.Singleton.Interfaces;
using FashionFace.Services.Singleton.Models;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class ErrorResponseService : IErrorResponseService
{
    public HttpErrorModel CreateErrorResponse(
        Exception exception,
        string traceId
    )
    {
        var responseCode =
            GetResponseCode(
                exception
            );

        var errorResponse =
            GetErrorsContainer(
                exception,
                traceId
            );

        return
            new(
                errorResponse,
                responseCode
            );
    }

    private static int GetResponseCode(
        Exception exception
    ) =>
        exception switch
        {
            BusinessLogicException =>
                HttpResponseCodeConstants.BadRequest,
            _ =>
                HttpResponseCodeConstants.ServerError,
        };

    private static ErrorsContainerModel GetErrorsContainer(
        Exception exception,
        string traceId
    )
    {
        var errors =
            GetErrors(
                exception
            );

        return
            new(
                traceId,
                errors
            );
    }

    private static ErrorModel GetErrors(
        Exception exception
    )
    {
        switch (exception)
        {
            case BusinessLogicException internalException:
            {
                return
                    new(
                        internalException.Code,
                        internalException.Data
                    );
            }
            default:
            {
                return
                    new(
                        ErrorCodeConstants.ServerError,
                        new Dictionary<string, object>()
                    );
            }
        }
    }
}