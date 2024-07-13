using Factorio.Modding.Api.Json.Prototypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Benchmarks
{
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default )]
    [JsonSerializable(typeof(PrototypeApi))]
    internal partial class PrototypeApiSourceGenerationContext : JsonSerializerContext
    {
    }

    internal class SourceGenerationOptions
    {
        public JsonSerializerOptions Options { get; }

        public SourceGenerationOptions()
        {
            Options = new JsonSerializerOptions()
            {
                TypeInfoResolver = PrototypeApiSourceGenerationContext.Default
            };

            FactorioJsonOptions.SetExisting(Options);
        }
    }
}
