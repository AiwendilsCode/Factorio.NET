using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class EmbeddedRuntimeCustomType
    {
        [JsonPropertyName("value")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Value { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
    }
}
