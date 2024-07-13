using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioAttribute : BasicMember
    {
        [JsonPropertyName("visibility")]
        public string[]? Visibility { get; init; }
        [JsonPropertyName("raises")]
        public EventRaised[]? Raises { get; init; }
        [JsonPropertyName("subclasses")]
        public string[]? SubClasses { get; init; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Type { get; init; }
        [JsonPropertyName("optional")]
        public required bool Optional { get; init; }
        [JsonPropertyName("read")]
        public required bool Read { get; init; }
        [JsonPropertyName("write")]
        public required bool Write { get; init; }
    }
}
