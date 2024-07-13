using System.Text.Json;
using Factorio.Modding.Api.Json.Converters;

namespace Factorio.Modding.Api.Json
{
    public class FactorioJsonOptions
    {
        public JsonSerializerOptions Options { get; }

        public FactorioJsonOptions()
        {
            Options = new JsonSerializerOptions();
            SetExisting(Options);
        }

        public static void SetExisting(JsonSerializerOptions options)
        {
            options.Converters.Add(new FactorioPrototypeCustomTypeConverter());
            options.Converters.Add(new FactorioRuntimeCustomTypeConverter());
            options.Converters.Add(new ListModeDependencyConverter());
            options.Converters.Add(new FactorioOperatorConverter());
        }
    }
}
