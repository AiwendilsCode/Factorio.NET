using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Mod
{
    public class FactorioModInfo
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("Version")]
        public required Version Version { get; set; }
        [JsonPropertyName("title")]
        public required string Title { get; set; }
        [JsonPropertyName("author")]
        public required string[] Authors { get; set; }
        [JsonPropertyName("contact")]
        public string? Contact { get; set; }
        [JsonPropertyName("homepage")]
        public string? Homepage { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("factorio_version")]
        public Version? FactorioVersion { get; set; }
        [JsonPropertyName("dependencies")]
        [JsonConverter(typeof(ListModeDependencyConverter))]
        public List<ModDependency>? Dependencies { get; set; }
    }
}
