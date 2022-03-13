using System.Text.Json.Serialization;

namespace Library.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }
    }
}
