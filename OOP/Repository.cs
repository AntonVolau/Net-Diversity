using Files.Documents;
using Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OOP
{
    class Repository: IRepository
    {
        public void Write<T>(IEnumerable<T> documents) where T : Document
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (var document in documents)
            {
                var jsonString = JsonSerializer.Serialize(document, options);
                File.WriteAllText($"{typeof(T).Name}_#{{{document.Id}}}.json", jsonString);
            }
        }

        public IEnumerable<T> Read<T>() where T : Document
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = directory
                .GetFiles("*.json")
                .Where(x => x.Name.StartsWith(typeof(T).Name));

            var documents = new List<T>();
            foreach (var file in files)
            {
                var jsonString = File.ReadAllText(file.FullName);
                var document = JsonSerializer.Deserialize<T>(jsonString)!;
                documents.Add(document);
            }
            return documents;
        }
    }
}
