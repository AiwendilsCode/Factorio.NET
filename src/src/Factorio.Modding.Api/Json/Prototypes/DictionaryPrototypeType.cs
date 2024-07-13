using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class DictionaryPrototypeType
    {
        [JsonPropertyName("key")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType Key { get; init; }
        [JsonPropertyName("value")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType Value { get; init; }
    }
}
