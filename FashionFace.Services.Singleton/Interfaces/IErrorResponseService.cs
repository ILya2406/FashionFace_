using System;

using FashionFace.Services.Singleton.Models;

namespace FashionFace.Services.Singleton.Interfaces;

public interface IErrorResponseService
{
    HttpErrorModel CreateErrorResponse(
        Exception exception,
        string traceId
    );
}