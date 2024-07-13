using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioMethod : BasicMember
    {
        [JsonPropertyName("visibility")]
        public string[]? Visibility { get; init; }
        [JsonPropertyName("raises")]
        public EventRaised[]? Raises { get; init; }
        [JsonPropertyName("subclasses")]
        public string[]? SubClasses { get; init; }
        [JsonPropertyName("parameters")]
        public required Parameter[] Parameters { get; init; }
        [JsonPropertyName("variant_parameter_groups")]
        public ParameterGroup[]? VariantParameterGroups { get; init; }
        [JsonPropertyName("variant_parameter_description")]
        public string? VariantParameterDescription { get; init; }
        [JsonPropertyName("format")]
        public required FactorioMethodFormat Format { get; init; }
        [JsonPropertyName("return_values")]
        public required Parameter[] ReturnValues { get; init; }
    }
}
