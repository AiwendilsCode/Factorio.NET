using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class LuaStructType
    {
        [JsonPropertyName("attributes")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioAttribute[] Attributes { get; init; }
    }
}
