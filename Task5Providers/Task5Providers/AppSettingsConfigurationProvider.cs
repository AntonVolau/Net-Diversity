using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using PluginBase;
using Formatting = Newtonsoft.Json.Formatting;

namespace Task5Providers
{
    public class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        public ProviderType ProviderType { get; } = ProviderType.AppSeting;

        public string FilePath { get; set; }

        public object Read(string key)
        {
            var json = File.ReadAllText(FilePath);
            dynamic jsonObj = JsonConvert.DeserializeObject<JObject>(json);
            return jsonObj[key];
        }

        public void Write(string key, object value)
        {
            var json = File.ReadAllText(FilePath);
            dynamic jsonObj = JsonConvert.DeserializeObject<JObject>(json);
            jsonObj[key] = null;
            if (value != null)
            {
                jsonObj[key] = value.ToString();
            }
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(FilePath, output);
        }
    }
}
