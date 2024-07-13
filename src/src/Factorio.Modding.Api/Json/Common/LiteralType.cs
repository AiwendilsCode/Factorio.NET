using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Common
{
    public class LiteralType
    {
        [JsonPropertyName("value")]
        public required object Value { get; init; }
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}
