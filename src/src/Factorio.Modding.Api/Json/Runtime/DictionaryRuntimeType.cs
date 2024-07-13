using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class DictionaryRuntimeType
    {
        [JsonPropertyName("key")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Key { get; init; }
        [JsonPropertyName("value")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Value { get; init; }
    }
}
