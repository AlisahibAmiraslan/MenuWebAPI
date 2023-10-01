using System.Text.Json.Serialization;

namespace MenuWebAPI.Models
{
    public class Backpack
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
