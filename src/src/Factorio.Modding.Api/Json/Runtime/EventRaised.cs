using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class EventRaised
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("order")]
        public required int Order { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("timeframe")]
        public required string TimeFrame { get; init; }
        [JsonPropertyName("optional")]
        public required bool Optional { get; init; }
    }
}
