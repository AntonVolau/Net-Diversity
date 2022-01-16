using System;
using PluginBase;
using Task5.CustomAttributes;
using Task5.Interfaces;

namespace Task5.Components
{
    public class File : ConfigurationComponentBase
    {
        public File(IProvidersFactory providersFactory) : base(providersFactory)
        {

        }

        [ConfigurationItem(ProviderType.File, "SomeString")]
        public override string SomeString
        {
            get => base.SomeString;
            set => base.SomeString = value;
        }

        [ConfigurationItem(ProviderType.File, "SomeTimespan")]
        public override TimeSpan? SomeTimespan
        {
            get => base.SomeTimespan;
            set => base.SomeTimespan = value;
        }

        [ConfigurationItem(ProviderType.File, "SomeInt")]
        public override int? SomeInt
        {
            get => base.SomeInt;
            set => base.SomeInt = value;
        }

        [ConfigurationItem(ProviderType.File, "SomeFloat")]
        public override float? SomeFloat
        {
            get => base.SomeFloat;
            set => base.SomeFloat = value;
        }
    }
}
