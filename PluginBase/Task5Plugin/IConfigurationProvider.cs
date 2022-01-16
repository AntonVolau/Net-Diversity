namespace PluginBase
{
    public interface IConfigurationProvider
    {
        public ProviderType ProviderType { get; }

        string FilePath { get; set; }

        object Read(string key);

        void Write(string key, object value);
    }

    public enum ProviderType
    {
        File,
        AppSeting
    }
}
