using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FunctionType
    {
        [JsonPropertyName("parameters")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType[] Parameters { get; init; }
    }
}
