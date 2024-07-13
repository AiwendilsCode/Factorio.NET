using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class ParameterGroup
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("order")]
        public required int Order { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("parameters")]
        public required Parameter[] Parameters { get; init; }
    }
}
