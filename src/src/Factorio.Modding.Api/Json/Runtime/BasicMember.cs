using Factorio.Modding.Api.Json.Common;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public class BasicMember
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        /// <summary>
        /// Must have default value of 0, because attributes in LuaStructType do not have this property.
        /// </summary>
        [JsonPropertyName("order")] 
        public int Order { get; init; } = 0;
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        [JsonPropertyName("lists")]
        public string[]? Lists { get; init; }
        [JsonPropertyName("examples")]
        public string[]? Examples { get; init; }
        [JsonPropertyName("images")]
        public Image[]? Images { get; init; }
    }
}
