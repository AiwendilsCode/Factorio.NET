using Factorio.Modding.Api.Json.Converters;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class UnionPrototypeType
    {
        [JsonPropertyName("options")]
        [JsonConverter(typeof(FactorioPrototypeCustomTypeConverter))]
        public required FactorioPrototypeCustomType[] Options { get; init; }
        [JsonPropertyName("full_format")]
        public required bool FullFormat { get; init; }
    }
}
