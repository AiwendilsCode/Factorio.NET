using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioClass : BasicMember
    {
        [JsonPropertyName("visibility")]
        public string[]? Visibility { get; init; }
        [JsonPropertyName("parent")]
        public string? Parent { get; init; }
        [JsonPropertyName("abstract")]
        public required bool Abstract { get; init; }
        [JsonPropertyName("methods")]
        public required FactorioMethod[] Methods { get; init; }
        [JsonPropertyName("attributes")]
        public required FactorioAttribute[] Attributes { get; init; }
        [JsonPropertyName("operators")]
        [JsonConverter(typeof(FactorioOperatorConverter))]
        public required object[] Operators { get; init; }
    }
}
