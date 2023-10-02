using System.Text.Json.Serialization;


namespace todoApi.Models
{
    public class TodoItem
    {
        [JsonPropertyName("id")]
         public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
