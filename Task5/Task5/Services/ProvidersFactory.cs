using System.Linq;
using PluginBase;
using Task5.Interfaces;

namespace Task5.Services
{
    public class ProvidersFactory : IProvidersFactory
    {
        private readonly IProvidersLoader _providersLoader;

        public ProvidersFactory(IProvidersLoader providersLoader)
        {
            _providersLoader = providersLoader;
        }

        public IConfigurationProvider GetProvider(ProviderType providerType)
        {
            var providers = _providersLoader.LoadProviders(@"..\..\..\Plugins\Providers.dll");
            var provider = providers.FirstOrDefault(x => x.ProviderType == providerType);
            if (providerType == ProviderType.AppSeting)
            {
                provider.FilePath = @"..\..\..\appsettings.json";
            }
            if (providerType == ProviderType.File)
            {
                provider.FilePath = @"..\..\..\custom.xml";
            }
            return provider;
        }
    }
}
