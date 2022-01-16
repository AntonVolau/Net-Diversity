using System;
using Task5.CustomAttributes;
using Task5.Interfaces;

namespace Task5.Components
{
    public abstract class ConfigurationComponentBase
    {
        private readonly IProvidersFactory _providersFactory;

        protected ConfigurationComponentBase(IProvidersFactory providersFactory)
        {
            _providersFactory = providersFactory;
        }

        public virtual string SomeString
        {
            get
            {
                var value = LoadSetting(nameof(SomeString));
                if (value != null)
                {
                    return value.ToString();
                }
                return null;
            }
            set => SaveSetting(nameof(SomeString), value);
        }

        public virtual TimeSpan? SomeTimespan
        {
            get
            {
                var value = LoadSetting(nameof(SomeTimespan));
                if (value != null)
                {
                    if (TimeSpan.TryParse(value.ToString(), out TimeSpan timeSpan))
                    {
                        return timeSpan;
                    }
                }
                return null;
            }
            set => SaveSetting(nameof(SomeTimespan), value);
        }

        public virtual int? SomeInt
        {
            get
            {
                var value = LoadSetting(nameof(SomeInt));
                if (value != null)
                {
                    if (int.TryParse(value.ToString(), out int someInt))
                    {
                        return someInt;
                    }
                }
                return null;
            }
            set => SaveSetting(nameof(SomeInt), value);
        }

        public virtual float? SomeFloat
        {
            get
            {
                var value = LoadSetting(nameof(SomeFloat));
                if (value != null)
                {
                    if (float.TryParse(value.ToString(), out float someFloat))
                    {
                        return someFloat;
                    }
                }
                return null;
            }
            set => SaveSetting(nameof(SomeFloat), value);
        }

        protected virtual object LoadSetting(string propertyName)
        {
            var attribute = GetAttribute(propertyName);
            if (attribute != null)
            {
                var provider = _providersFactory.GetProvider(attribute.Type);
                if (provider != null)
                {
                    return provider.Read(attribute.SettingName);
                }
            }
            return null;
        }

        protected virtual void SaveSetting(string propertyName, object value)
        {
            var attribute = GetAttribute(propertyName);
            if (attribute != null)
            {
                var provider = _providersFactory.GetProvider(attribute.Type);
                if (provider != null)
                {
                    provider.Write(attribute.SettingName, value);
                }
            }
        }

        private ConfigurationItemAttribute GetAttribute(string propertyName)
        {
            var type = GetType();
            var property = type.GetProperty(propertyName);
            if (property != null)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute))
                    as ConfigurationItemAttribute;
                return attribute;
            }
            return null;
        }
    }
}
