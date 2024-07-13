using Factorio.Modding.Api.Json.Runtime;
using FluentAssertions;
using System.Text.Json;
using Factorio.Modding.Api.Json;

namespace Factorio.Modding.Api.Tests.Json.ConverterTests
{
    public class FactorioRuntimeCustomTypeConverterTests
    {
        private readonly JsonSerializerOptions _options = new();

        public FactorioRuntimeCustomTypeConverterTests()
        {
            FactorioJsonOptions.SetExisting(_options);
        }

        [Fact]
        public void ConvertTableType()
        {
            // Arrange
            var json = @"{
""complex_type"": ""table"",
""parameters"": [
  {
    ""name"": ""type"",
    ""order"": 0,
    ""description"": """",
    ""type"": {
      ""complex_type"": ""union"",
      ""options"": [
        {
          ""complex_type"": ""literal"",
          ""value"": ""throw""
        }
      ],
      ""full_format"": false
    },
    ""optional"": false
  }
],
""variant_parameter_groups"": [
  {
    ""name"": ""artillery-remote"",
    ""order"": 3,
    ""description"": """",
    ""parameters"": [
      {
        ""name"": ""flare"",
        ""order"": 0,
        ""description"": ""Name of the [flare prototype](runtime:LuaEntityPrototype)."",
        ""type"": ""string"",
        ""optional"": false
      }
    ]
  }
],
""variant_parameter_description"": ""Other attributes may be specified depending on `type`:""
}";

            // Act
            var type = JsonSerializer.Deserialize<FactorioRuntimeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(RuntimeComplexTypeEnum.Table);
            Assert.True(type.Value is TableType);
            var table = (type.Value as TableType)!;
            table.Parameters.Should().HaveCount(1);
            table.VariantParameterDescription.Should().Be("Other attributes may be specified depending on `type`:");
            table.VariantParameterGroups.Should().HaveCount(1);
        }

        [Fact]
        public void ConvertFunctionType()
        {
            // Arrange
            var json = @"{
    ""complex_type"": ""function"",
    ""parameters"": [
      ""CustomCommandData""
    ]
  }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioRuntimeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(RuntimeComplexTypeEnum.Function);
            Assert.True(type.Value is FunctionType);
            var function = (type.Value as FunctionType)!;
            function.Parameters.Should().HaveCount(1);
        }

        [Fact]
        public void ConvertLuaLazyLoadedValueType()
        {
            // Arrange
            var json = @"{
    ""complex_type"": ""LuaLazyLoadedValue"",
    ""value"": {
      ""complex_type"": ""dictionary"",
      ""key"": ""uint"",
      ""value"": ""LuaEntity""
    }
  }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioRuntimeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(RuntimeComplexTypeEnum.LuaLazyLoadedValue);
            Assert.True(type.Value is LuaLazyLoadedValueType);
            var lazyLoadedValue = (type.Value as LuaLazyLoadedValueType)!;

            lazyLoadedValue.Value.ComplexType.Should().Be(RuntimeComplexTypeEnum.Dictionary);
            Assert.True(lazyLoadedValue.Value.Value is DictionaryRuntimeType);
            var dictionary = (lazyLoadedValue.Value.Value as DictionaryRuntimeType)!;

            Assert.Null(dictionary.Key.ComplexType);
            (dictionary.Key.Value as string).Should().Be("uint");
            Assert.Null(dictionary.Value.ComplexType);
            (dictionary.Value.Value as string).Should().Be("LuaEntity");
        }

        [Fact]
        public void ConvertLuaStructType()
        {
            // Arrange
            var json = @"{
    ""complex_type"": ""LuaStruct"",
    ""attributes"": [
      {
        ""name"": ""recipe_difficulty"",
        ""description"": """",
        ""type"": ""defines.difficulty_settings.recipe_difficulty"",
        ""optional"": false,
        ""read"": true,
        ""write"": true
      }
    ]
  }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioRuntimeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(RuntimeComplexTypeEnum.LuaStruct);
            Assert.True(type.Value is LuaStructType);
            var function = (type.Value as LuaStructType)!;
            function.Attributes.Should().HaveCount(1);
        }
    }
}
