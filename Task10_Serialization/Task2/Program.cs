using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Models;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var simple = new SimpleClass
            {
                Name = "simple",
                Count = 1
            };
            Serialize(simple, "simple.bin");
            var deserialized = Deserialize("simple.bin");
        }
        
        static void Serialize(SimpleClass simpleClass, string fileName)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, simpleClass);
            stream.Close();
        }

        static SimpleClass Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            var simpleClass = (SimpleClass) formatter.Deserialize(stream);
            stream.Close();
            return simpleClass;
        }
    }
}
