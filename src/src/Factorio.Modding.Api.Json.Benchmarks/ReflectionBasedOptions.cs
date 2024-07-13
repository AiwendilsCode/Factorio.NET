using System.Text.Json;

namespace Factorio.Modding.Api.Json.Benchmarks
{
    internal class ReflectionBasedOptions
    {
        public JsonSerializerOptions Options { get; }

        public ReflectionBasedOptions()
        {
            Options = new JsonSerializerOptions();

            FactorioJsonOptions.SetExisting(Options);
        }
    }
}
