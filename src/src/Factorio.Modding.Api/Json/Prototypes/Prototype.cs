using Factorio.Modding.Api.Json.Common;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class Prototype
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("order")]
        public int Order { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("lists")]
        public string[]? Lists { get; init; }
        [JsonPropertyName("examples")]
        public string[]? Examples { get; init; }
        [JsonPropertyName("images")]
        public Image[]? Images { get; init; }
        [JsonPropertyName("parent")]
        public string? Parent { get; init; }
        [JsonPropertyName("abstract")]
        public required bool Abstract { get; init; }
        [JsonPropertyName("typename")]
        public string? Typename { get; init; }
        [JsonPropertyName("instance_limit")]
        public int? InstanceLimit { get; init; }
        [JsonPropertyName("deprecated")]
        public required bool Deprecated { get; init; }
        [JsonPropertyName("properties")]
        public required FactorioProperty[] Properties { get; init; }
        [JsonPropertyName("custom_properties")]
        public CustomProperties? CustomProperties { get; init; }
    }
}
