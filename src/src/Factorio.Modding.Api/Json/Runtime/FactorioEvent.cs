using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioEvent : BasicMember
    {
        [JsonPropertyName("data")]
        public required Parameter[] Data { get; init; }
        [JsonPropertyName("filter")]
        public string? Filter { get; init; }
    }
}
