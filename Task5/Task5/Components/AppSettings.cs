using System;
using PluginBase;
using Task5.CustomAttributes;
using Task5.Interfaces;

namespace Task5.Components
{
    public class AppSettings: ConfigurationComponentBase
    {
        public AppSettings(IProvidersFactory providersFactory) : base(providersFactory)
        {

        }

        [ConfigurationItem(ProviderType.AppSeting, "SomeString")]
        public override string SomeString
        {
            get => base.SomeString;
            set => base.SomeString = value;
        }

        [ConfigurationItem(ProviderType.AppSeting, "SomeTimespan")]
        public override TimeSpan? SomeTimespan
        {
            get => base.SomeTimespan;
            set => base.SomeTimespan = value;
        }

        [ConfigurationItem(ProviderType.AppSeting, "SomeInt")]
        public override int? SomeInt
        {
            get => base.SomeInt;
            set => base.SomeInt = value;
        }

        [ConfigurationItem(ProviderType.AppSeting, "SomeFloat")]
        public override float? SomeFloat
        { 
            get => base.SomeFloat;
            set => base.SomeFloat = value;
        }
    }
}
