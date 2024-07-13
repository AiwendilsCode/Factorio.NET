using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class Define : BasicMember
    {
        [JsonPropertyName("values")]
        public DefineValue[]? Values { get; init; }
        [JsonPropertyName("subkeys")]
        public Define[]? SubKeys { get; init; }
    }
}
