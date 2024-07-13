using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class CustomProperties
    {
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("lists")]
        public string[]? Lists { get; init; }
        [JsonPropertyName("examples")]
        public string[]? Examples { get; init; }
        [JsonPropertyName("images")]
        public Image[]? Images { get; init; }
        [JsonPropertyName("key_type")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType KeyType { get; init; }
        [JsonPropertyName("value_type")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType ValueType { get; init; }
    }
}
