using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class RuntimeApi
    {
        [JsonIgnore]
        public bool IsOrdered { get; internal set; } = false;

        [JsonPropertyName("application")]
        public required string Application { get; init; }
        [JsonPropertyName("application_version")]
        public required string AppVersion { get; init; }
        [JsonPropertyName("api_version")]
        public required int ApiVersion { get; init; }
        [JsonPropertyName("stage")]
        public required string Stage { get; init; }
        [JsonPropertyName("classes")]
        public required FactorioClass[] Classes { get; init; }
        [JsonPropertyName("events")]
        public required FactorioEvent[] Events { get; init; }
        [JsonPropertyName("concepts")]
        public required Concept[] Concepts { get; init; }
        [JsonPropertyName("defines")]
        public required Define[] Defines { get; init; }
        [JsonPropertyName("global_objects")]
        public required Parameter[] GlobalObjects { get; init; }
        [JsonPropertyName("global_functions")]
        public required FactorioMethod[] GlobalFunctions { get; init; }
    }
}
