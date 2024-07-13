using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class PrototypeApi
    {
        [JsonIgnore]
        public bool IsOrdered { get; internal set; } = false;

        [JsonPropertyName("application")]
        public required string Application { get; init; }
        [JsonPropertyName("application_version")]
        public required string AppVersion { get; init; }
        [JsonPropertyName("api_version")]
        public required int ApiVersion { get; init; }
        [JsonPropertyName("stage")]
        public required string Stage { get; init; }

        [JsonPropertyName("prototypes")]
        public required Prototype[] Prototypes { get; init; }
        [JsonPropertyName("types")]
        public required PrototypeType[] Types { get; init; }
    }
}
