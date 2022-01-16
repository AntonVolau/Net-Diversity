using System.Xml;
using PluginBase;

namespace Task5Providers
{
    public class FileConfigurationProvider : IConfigurationProvider
    {
        public ProviderType ProviderType { get; } = ProviderType.File;

        public string FilePath { get; set; }

        public object Read(string key)
        {
            var doc = new XmlDocument();
            doc.Load(FilePath);
            var root = doc.DocumentElement;
            var node = root.SelectSingleNode(key);
            return node.InnerText;
        }

        public void Write(string key, object value)
        {
            var doc = new XmlDocument();
            doc.Load(FilePath);
            var root = doc.DocumentElement;
            var node = root.SelectSingleNode(key);
            if (node == null)
            {
                node = doc.CreateElement(key);
                root.AppendChild(node);
            }
            node.InnerText = null;
            if (value != null)
            {
                node.InnerText = value.ToString();
            }
            doc.Save(FilePath);
        }
    }
}
