using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class UnionRuntimeType
    {
        [JsonPropertyName("options")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType[] Options { get; set; }
        [JsonPropertyName("full_format")]
        public required bool FullFormat { get; init; }
    }
}
