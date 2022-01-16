using System;
using PluginBase;
using Task5.Components;
using Task5.CustomAttributes;
using Task5.Services;

namespace Task5
{
    class Program
    {
        static void Main()
        {
            var loader = new ProvidersLoader();
            var factory = new ProvidersFactory(loader);

            var appSettings = new AppSettings(factory);
            var file = new File(factory);

            WriteConfig(appSettings);
            ReadConfig(appSettings);
            Console.WriteLine();
            WriteConfig(file);
            ReadConfig(file);
        }

        public static void WriteConfig(ConfigurationComponentBase config)
        {
            config.SomeInt = 666;
            config.SomeTimespan = TimeSpan.FromSeconds(69);
            config.SomeString = "Nice task, I love it!";
            config.SomeFloat = 1234;
        }

        public static void ReadConfig(ConfigurationComponentBase config)
        {
            var someString = config.SomeString;
            var someInt = config.SomeInt;
            var someTimespan = config.SomeTimespan;
            var someFloat = config.SomeFloat;

            Console.WriteLine($"{nameof(config.SomeString)}: {someString}");
            Console.WriteLine($"{nameof(config.SomeInt)}: {someInt}");
            Console.WriteLine($"{nameof(config.SomeTimespan)}: {someTimespan}");
            Console.WriteLine($"{nameof(config.SomeFloat)}: {someFloat}");
            Console.ReadLine();
        }
    }
}
