using System.Collections.Generic;
using PluginBase;

namespace Task5.Interfaces
{
    public interface IProvidersLoader 
    {
        IEnumerable<IConfigurationProvider> LoadProviders(string pluginPath);
    }
}
