namespace FashionFace.Facades.Base.Interfaces;

public interface IQueryFacade<in TArgs, TResult>
{
    Task<TResult> Execute(
        TArgs args
    );
}