using System.Text.Json;
using System.Text.Json.Serialization;
using Factorio.Modding.Api.Mod;

namespace Factorio.Modding.Api.Json.Converters
{
    internal class ListModeDependencyConverter : JsonConverter<List<ModDependency>>
    {
        public override List<ModDependency>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<ModDependency> dependencies = [];

            while (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Read();
            }

            reader.Read(); // read one token to be at first element

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                dependencies.Add(ModDependency.Parse(reader.GetString()!, null));
                reader.Read();
            }

            return dependencies;
        }

        public override void Write(Utf8JsonWriter writer, List<ModDependency> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (ModDependency dependency in value)
            {
                writer.WriteStringValue(dependency.ToString());
            }
            writer.WriteEndArray();
        }
    }
}
