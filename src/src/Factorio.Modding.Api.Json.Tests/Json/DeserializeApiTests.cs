using Factorio.Modding.Api.Json.Prototypes;
using Factorio.Modding.Api.Json.Runtime;
using System.Text.Json;
using Factorio.Modding.Api.Json;

namespace Factorio.Modding.Api.Tests.Json
{
    public class DeserializeApiTests
    {
        private readonly JsonSerializerOptions _options = new();

        public DeserializeApiTests()
        {
            FactorioJsonOptions.SetExisting(_options);
        }

        [Fact]
        public void DeserializePrototypeApi()
        {
            // Arrange
            var json = File.ReadAllText("Json\\ApiDocs\\prototype-api.json");

            // Act
            var prototypeApi = JsonSerializer.Deserialize<PrototypeApi>(json, _options);

            // Assert
            Assert.NotNull(prototypeApi);
        }

        [Fact]
        public void DeserializeRuntimeApi()
        {
            // Arrange
            var json = File.ReadAllText("Json\\ApiDocs\\runtime-api.json");

            // Act
            var runtimeApi = JsonSerializer.Deserialize<RuntimeApi>(json, _options);

            // Assert
            Assert.NotNull(runtimeApi);
        }
    }
}
