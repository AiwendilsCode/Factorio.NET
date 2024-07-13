using Factorio.Modding.Api.Json.Runtime;
using FluentAssertions;
using System.Text.Json;
using Factorio.Modding.Api.Json;

namespace Factorio.Modding.Api.Tests.Json.ConverterTests
{
    public class FactorioOperatorConverterTests
    {
        private readonly JsonSerializerOptions _options = new();

        public FactorioOperatorConverterTests()
        {
            FactorioJsonOptions.SetExisting(_options);
        }

        [Fact]
        public void DeserializeMethodOperator()
        {
            // Arrange
            var json = @"[
    {
      ""name"": ""call"",
      ""order"": 0,
      ""description"": ""Gets the next chunk position if the iterator is not yet done and increments the iterator."",
      ""parameters"": [
      ],
      ""format"": {
        ""takes_table"": false
      },
      ""return_values"": [
        {
          ""order"": 0,
          ""description"": """",
          ""type"": ""ChunkPositionAndArea"",
          ""optional"": true
        }
      ]
    }
]";

            // Act
            var deserialized = JsonSerializer.Deserialize<object[]>(json, _options);

            // Assert
            deserialized.Should().HaveCount(1);
            Assert.True(deserialized!.First() is FactorioMethod);
            var method = (deserialized!.First() as FactorioMethod)!;

            method.Name.Should().Be("call");
            method.Order.Should().Be(0);
            method.Description.Should().Be("Gets the next chunk position if the iterator is not yet done and increments the iterator.");
            method.Parameters.Should().HaveCount(0);
            method.Format.TakesTable.Should().BeFalse();
            Assert.Null(method.Format.TableOptional);
            method.ReturnValues.Should().HaveCount(1);
            Assert.Null(method.ReturnValues.First().Name);
            method.ReturnValues.First().Description.Should().Be("");
            method.ReturnValues.First().Optional.Should().BeTrue();
            method.ReturnValues.First().Order.Should().Be(0);
            Assert.Null(method.ReturnValues.First().Type.ComplexType);
            (method.ReturnValues.First().Type.Value as string).Should().Be("ChunkPositionAndArea");
            Assert.Null(method.Raises);
            Assert.Null(method.SubClasses);
            Assert.Null(method.VariantParameterGroups);
            Assert.Null(method.VariantParameterDescription);
            Assert.Null(method.Visibility);
            Assert.Null(method.Examples);
            Assert.Null(method.Images);
            Assert.Null(method.Lists);
        }

        [Fact]
        public void DeserializeAttributeOperator()
        {
            // Arrange
            var json = @"[
    {
      ""name"": ""index"",
      ""order"": 0,
      ""description"": ""Access an element of this custom table."",
      ""type"": ""Any"",
      ""optional"": false,
      ""read"": true,
      ""write"": false
    }
  ]";

            // Act
            var deserialized = JsonSerializer.Deserialize<object[]>(json, _options);

            // Assert
            deserialized.Should().HaveCount(1);
            Assert.True(deserialized!.First() is FactorioAttribute);
            var attribute = (deserialized!.First() as FactorioAttribute)!;

            attribute.Name.Should().Be("index");
            attribute.Order.Should().Be(0);
            attribute.Description.Should().Be("Access an element of this custom table.");
            Assert.Null(attribute.Type.ComplexType);
            (attribute.Type.Value as string).Should().Be("Any");
            attribute.Optional.Should().BeFalse();
            attribute.Read.Should().BeTrue();
            attribute.Write.Should().BeFalse();
            Assert.Null(attribute.Raises);
            Assert.Null(attribute.SubClasses);
            Assert.Null(attribute.Visibility);
            Assert.Null(attribute.Examples);
            Assert.Null(attribute.Lists);
            Assert.Null(attribute.Images);
        }
    }
}
