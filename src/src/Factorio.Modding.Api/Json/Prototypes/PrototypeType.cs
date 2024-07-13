using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class PrototypeType
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("order")]
        public required int Order { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("lists")]
        public string[]? Lists { get; init; }
        [JsonPropertyName("examples")]
        public string[]? Examples { get; init; }
        [JsonPropertyName("images")]
        public Image[]? Images { get; init; }
        [JsonPropertyName("parent")]
        public string? Parent { get; init; }
        [JsonPropertyName("abstract")]
        public required bool Abstract { get; init; }
        [JsonPropertyName("inline")]
        public required bool Inline { get; init; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType Type { get; init; }
        [JsonPropertyName("properties")]
        public FactorioProperty[]? Properties { get; init; }
    }
}
