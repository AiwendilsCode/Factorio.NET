using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class FactorioProperty
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
        [JsonPropertyName("alt_name")]
        public string? AltName { get; init; }
        [JsonPropertyName("override")]
        public required bool Override { get; init; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType Type { get; init; }
        [JsonPropertyName("optional")]
        public required bool Optional { get; init; }
        [JsonPropertyName("default")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public FactorioPrototypeCustomType? Default { get; init; }
    }
}
