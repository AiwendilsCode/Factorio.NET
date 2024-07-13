using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioRuntimeCustomType
    {
        [JsonPropertyName("complex_type")]
        public required RuntimeComplexTypeEnum? ComplexType { get; init; }
        public required object? Value { get; init; }
    }
}
