using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class FactorioMethodFormat
    {
        [JsonPropertyName("takes_table")]
        public required bool TakesTable { get; init; }
        [JsonPropertyName("table_optional")]
        public bool? TableOptional { get; init; }
    }
}
