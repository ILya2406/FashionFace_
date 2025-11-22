namespace FashionFace.Facades.Base.Interfaces;

public interface ICommandFacade<in TArgs>
{
    Task Execute(
        TArgs args
    );
}