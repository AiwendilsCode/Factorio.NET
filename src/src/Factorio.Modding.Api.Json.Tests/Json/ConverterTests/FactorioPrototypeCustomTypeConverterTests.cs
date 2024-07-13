using System.Text.Json;
using Factorio.Modding.Api.Json;
using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Prototypes;
using FluentAssertions;

namespace Factorio.Modding.Api.Tests.Json.ConverterTests
{
    internal class MyLocalJsonOptions
    {
        public JsonSerializerOptions Options { get; }

        public MyLocalJsonOptions()
        {
            Options = new JsonSerializerOptions();

            FactorioJsonOptions.SetExisting(Options);
        }
    }

    public class FactorioPrototypeCustomTypeConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public FactorioPrototypeCustomTypeConverterTests()
        {
            _options = new MyLocalJsonOptions().Options;
        }

        [Fact]
        public void ConvertArrayType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""array"",
                ""value"": ""CharacterArmorAnimation""
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Array);
            ((type.Value as FactorioPrototypeCustomType)!.Value as string).Should().Be("CharacterArmorAnimation");
        }

        [Fact]
        public void ConvertDictionaryType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""dictionary"",
                ""key"": ""ItemID"",
                ""value"": ""int32""
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Dictionary);
            ((type.Value as DictionaryPrototypeType)!.Key.Value as string).Should().Be("ItemID");
            ((type.Value as DictionaryPrototypeType)!.Value.Value as string).Should().Be("int32");
        }

        [Fact]
        public void ConvertTupleType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""tuple"",
                ""values"": [
                    ""CircuitConnectorSprites"",
                    ""CircuitConnectorSprites"",
                    ""CircuitConnectorSprites"",
                    ""CircuitConnectorSprites""
                ]
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Tuple);
            (type.Value as FactorioPrototypeCustomType[]).Should().HaveCount(4);
        }

        [Fact]
        public void ConvertUnionType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""union"",
                ""options"": [
                    {
                        ""complex_type"": ""literal"",
                        ""value"": ""default"",
                        ""description"": ""Items are inserted into this item-with-inventory only if they match the whitelist defined in the prototype for the item and whitelist is used.""
                    },
                    {
                        ""complex_type"": ""literal"",
                        ""value"": ""never"",
                        ""description"": ""Items are never inserted into this item-with-inventory except explicitly by the player or script.""
                    },
                    {
                        ""complex_type"": ""literal"",
                        ""value"": ""always"",
                        ""description"": ""All items first try to be inserted into this item-with-inventory.""
                    },
                    {
                        ""complex_type"": ""literal"",
                        ""value"": ""when-manually-filtered"",
                        ""description"": ""When the inventory contains filters that match the item-to-be-inserted then try to put it into this item before the inventory this item resides in.""
                    }
                ],
                ""full_format"": true
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Union);
            (type.Value as UnionPrototypeType)!.FullFormat.Should().BeTrue();
            (type.Value as UnionPrototypeType)!.Options.Should().HaveCount(4);
        }

        [Fact]
        public void ConvertLiteralType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""literal"",
                ""value"": ""always"",
                ""description"": ""All items first try to be inserted into this item-with-inventory.""
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Literal);
            ((type.Value as LiteralType)!.Value as string).Should().Be("always");
            (type.Value as LiteralType)!.Description.Should().Be("All items first try to be inserted into this item-with-inventory.");
        }

        [Fact]
        public void ConvertEmbeddedCustomType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""type"",
                ""value"": ""PrototypeBase"",
                ""description"": """"
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Type);
            ((type.Value as EmbeddedPrototypeCustomType)!.Value.Value as string).Should().Be("PrototypeBase");
            (type.Value as EmbeddedPrototypeCustomType)!.Description.Should().Be("");
        }

        [Fact]
        public void ConvertStructType_shouldConvert()
        {
            // Arrange
            var json = @"
            {
                ""complex_type"": ""struct""
            }";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            type!.ComplexType.Should().Be(PrototypeComplexTypeEnum.Struct);
            Assert.Null(type!.Value);
        }

        [Fact]
        public void ConvertStringType_shouldConvert()
        {
            // Arrange
            var json = @"""RenderLayer""";

            // Act
            var type = JsonSerializer.Deserialize<FactorioPrototypeCustomType>(json, _options);

            // Assert
            Assert.Null(type!.ComplexType);
            (type.Value as string).Should().Be("RenderLayer");
        }
    }
}
