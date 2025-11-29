using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;
using FashionFace.Facades.Models;
using FashionFace.Repositories.Read.Interfaces;

namespace FashionFace.Facades.Implementations;

public sealed class RegisterFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IRegisterFacade
{
    public Task<RegisterResult> Execute(
        RegisterArgs args
    )
    {
        throw new System.NotImplementedException();
    }
}