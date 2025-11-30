using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Base.Interfaces;

namespace FashionFace.Facades.Anonymous.Interfaces;

public interface IRegisterFacade :
    ICommandFacade
    <
        RegisterArgs
    >;