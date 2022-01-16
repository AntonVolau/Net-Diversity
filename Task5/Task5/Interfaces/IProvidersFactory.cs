using PluginBase;

namespace Task5.Interfaces
{
    public interface IProvidersFactory
    {
        IConfigurationProvider GetProvider(ProviderType providerType);
    }
}
