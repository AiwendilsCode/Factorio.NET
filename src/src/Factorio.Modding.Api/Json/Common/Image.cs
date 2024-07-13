using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Common
{
    public class Image
    {
        [JsonPropertyName("filename")]
        public required string Filename { get; init; }
        [JsonPropertyName("caption")]
        public string? Caption { get; init; }
    }
}
