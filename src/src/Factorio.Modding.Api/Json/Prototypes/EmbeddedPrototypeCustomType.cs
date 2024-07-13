using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class EmbeddedPrototypeCustomType
    {
        [JsonPropertyName("value")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType Value { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
    }
}
