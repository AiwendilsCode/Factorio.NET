using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class Concept : BasicMember
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Type { get; init; }
    }
}
