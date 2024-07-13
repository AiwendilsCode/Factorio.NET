using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class Parameter
    {
        /// <summary>
        /// Can be null only in return values
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }
        [JsonPropertyName("order")]
        public required int Order { get; init; }
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(FactorioRuntimeCustomTypeConverter))]
        public required FactorioRuntimeCustomType Type { get; init; }
        /// <summary>
        /// If null, check FactorioMethod.Format property how to handle this value,
        /// since it can be null only in FactorioMethod.Parameters.
        /// </summary>
        [JsonPropertyName("optional")]
        public bool? Optional { get; init; }
    }
}
