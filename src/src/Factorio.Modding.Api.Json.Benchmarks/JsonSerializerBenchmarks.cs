using BenchmarkDotNet.Attributes;
using Factorio.Modding.Api.Json.Prototypes;
using System.Text.Json;

namespace Factorio.Modding.Api.Json.Benchmarks
{
    [MemoryDiagnoser]
    public class JsonSerializerBenchmarks
    {
        private JsonSerializerOptions _sourceGenOptions = new SourceGenerationOptions().Options;
        private JsonSerializerOptions _reflectionOptions = new ReflectionBasedOptions().Options;

        private string _jsonApi = File.ReadAllText("ApiDocs\\prototype-api.json");

        [Benchmark]
        public void DeserializeSourceGeneration_100()
        {
            for (int i = 0; i < 100; i++)
            {
                _ = JsonSerializer.Deserialize<PrototypeApi>(_jsonApi, _sourceGenOptions);
            }
        }

        [Benchmark]
        public void DeserializeReflection_100()
        {
            for (int i = 0; i < 100; i++)
            {
                _ = JsonSerializer.Deserialize<PrototypeApi>(_jsonApi, _reflectionOptions);
            }
        }
    }
}
