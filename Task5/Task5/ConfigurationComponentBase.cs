using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class ConfigurationComponentBase
    {
        public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static void CreateConfigurationFile()
        {
            try
            {

                // Create a custom configuration section.
                CustomSection customSection = new CustomSection();

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                // Create the custom section entry  
                // in <configSections> group and the 
                // related target section in <configuration>.
                if (config.Sections["CustomSection"] == null)
                {
                    config.Sections.Add("CustomSection", customSection);
                }

                // Create and add an entry to appSettings section.

                string conStringname = "LocalSqlServer";
                string conString = @"data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
                string providerName = "System.Data.SqlClient";

                ConnectionStringSettings connStrSettings = new ConnectionStringSettings();
                connStrSettings.Name = conStringname;
                connStrSettings.ConnectionString = conString;
                connStrSettings.ProviderName = providerName;

                config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

                // Add an entry to appSettings section.
                int appStgCnt =
                    ConfigurationManager.AppSettings.Count;
                string newKey = "NewKey" + appStgCnt.ToString();

                string newValue = DateTime.Now.ToLongDateString() +
                  " " + DateTime.Now.ToLongTimeString();

                config.AppSettings.Settings.Add(newKey, newValue);

                // Save the configuration file.
                customSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);

                Console.WriteLine("Created configuration file: {0}",
                    config.FilePath);
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("CreateConfigurationFile: {0}", err.ToString());
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CustomAttribute2 : Attribute
    {
        public int Age { get; set; }

        public CustomAttribute2()
        { }

        public CustomAttribute2(int age)
        {
            Age = age;
        }
    }

    public sealed class CustomSection :
        ConfigurationSection
    {
        // The collection (property bag) that contains 
        // the section properties.
        private static ConfigurationPropertyCollection _Properties;

        // The FileName property.
        private static readonly ConfigurationProperty _FileName =
            new ConfigurationProperty("fileName",
            typeof(string), "default.txt",
            ConfigurationPropertyOptions.IsRequired);

        // The MasUsers property.
        private static readonly ConfigurationProperty _MaxUsers =
            new ConfigurationProperty("maxUsers",
            typeof(long), (long)1000,
            ConfigurationPropertyOptions.None);

        // The MaxIdleTime property.
        private static readonly ConfigurationProperty _MaxIdleTime =
            new ConfigurationProperty("maxIdleTime",
            typeof(TimeSpan), TimeSpan.FromMinutes(5),
            ConfigurationPropertyOptions.IsRequired);

        // CustomSection constructor.
        public CustomSection()
        {
            // Property initialization
            _Properties =
                new ConfigurationPropertyCollection();

            _Properties.Add(_FileName);
            _Properties.Add(_MaxUsers);
            _Properties.Add(_MaxIdleTime);
        }

        // This is a key customization. 
        // It returns the initialized property bag.
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\",
            MinLength = 1, MaxLength = 60)]
        public string FileName
        {
            get
            {
                return (string)this["fileName"];
            }
            set
            {

                this["fileName"] = value;
            }
        }

        [LongValidator(MinValue = 1, MaxValue = 1000000,
            ExcludeRange = false)]
        public long MaxUsers
        {
            get
            {
                return (long)this["maxUsers"];
            }
            set
            {
                this["maxUsers"] = value;
            }
        }

        [TimeSpanValidator(MinValueString = "0:0:30",
            MaxValueString = "5:00:0",
            ExcludeRange = false)]
        public TimeSpan MaxIdleTime
        {
            get
            {
                return (TimeSpan)this["maxIdleTime"];
            }
            set
            {
                this["maxIdleTime"] = value;
            }
        }
    }
}
