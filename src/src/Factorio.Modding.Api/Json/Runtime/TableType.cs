using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class TableType
    {
        [JsonPropertyName("parameters")]
        public required Parameter[] Parameters { get; init; }
        [JsonPropertyName("variant_parameter_groups")]
        public ParameterGroup[]? VariantParameterGroups { get; init; }
        [JsonPropertyName("variant_parameter_description")]
        public string? VariantParameterDescription { get; init; }
    }
}
