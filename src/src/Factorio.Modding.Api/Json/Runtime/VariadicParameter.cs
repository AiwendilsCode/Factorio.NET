using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class VariadicParameter
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public FactorioRuntimeCustomType? Type { get; init; }
        [JsonPropertyName("return_values")]
        public required Parameter[]? ReturnValues { get; set; }
    }
}
