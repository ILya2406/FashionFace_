namespace FashionFace.Services.ConfigurationSettings.Interfaces;

public interface ISettingsFactoryBase<out TResult>
    where TResult : class
{
    TResult GetSettings();
}