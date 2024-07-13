using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public class FactorioPrototypeCustomType
    {
        [JsonPropertyName("complex_type")]
        public PrototypeComplexTypeEnum? ComplexType { get; init; }
        public object? Value { get; set; }
    }
}
