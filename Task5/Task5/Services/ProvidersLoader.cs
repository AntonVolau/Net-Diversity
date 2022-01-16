using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PluginBase;
using Task5.Interfaces;

namespace Task5.Services
{
    public class ProvidersLoader : IProvidersLoader
    {
        public IEnumerable<IConfigurationProvider> LoadProviders(string pluginPath)
        {
            var assembly = Assembly.LoadFrom(pluginPath);
            var types = assembly.GetTypes()
                .Where(x => typeof(IConfigurationProvider).IsAssignableFrom(x));
            var providers = new List<IConfigurationProvider>();
            foreach (var type in types)
            {
                var provider = Activator.CreateInstance(type) as IConfigurationProvider;
                providers.Add(provider);
            }
            return providers;
        }
    }
}
