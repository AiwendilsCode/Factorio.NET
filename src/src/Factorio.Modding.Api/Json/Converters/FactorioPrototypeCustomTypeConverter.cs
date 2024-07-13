using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Prototypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Converters
{
    // In every read method I do one additional reader.Read()
    // This is because, in ReadLiteral method it is required,
    // thus I am doing it everywhere else, to avoid
    // some sketchy if statements or tricks.
    internal class FactorioPrototypeCustomTypeConverter : JsonConverter<FactorioPrototypeCustomType>
    {
        public override FactorioPrototypeCustomType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = ReadType(ref reader, options);

            return result;
        }

        public override void Write(Utf8JsonWriter writer, FactorioPrototypeCustomType value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private FactorioPrototypeCustomType GetFactorioTypeValue(PrototypeComplexTypeEnum complexType, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            object? value = complexType switch
            {
                PrototypeComplexTypeEnum.Array => ReadArray(ref reader, options),
                PrototypeComplexTypeEnum.Dictionary => ReadDictionary(ref reader, options),
                PrototypeComplexTypeEnum.Tuple => ReadTuple(ref reader, options),
                PrototypeComplexTypeEnum.Union => ReadUnion(ref reader, options),
                PrototypeComplexTypeEnum.Literal => ReadLiteral(ref reader, options),
                PrototypeComplexTypeEnum.Type => ReadEmbeddedType(ref reader, options),
                PrototypeComplexTypeEnum.Struct => ReadStruct(ref reader),
                _ => throw new NotSupportedException($"Not supported complex type: {complexType}.")
            };

            return new FactorioPrototypeCustomType { ComplexType = complexType, Value = value, };
        }

        private object? ReadStruct(ref Utf8JsonReader reader)
        {
            reader.Read();
            return null;
        }

        private FactorioPrototypeCustomType ReadType(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var strValue = reader.GetString();

                return new FactorioPrototypeCustomType
                {
                    ComplexType = null,
                    Value = strValue!
                };
            }

            while (reader.TokenType != JsonTokenType.PropertyName) // complex_type
            {
                reader.Read();
            }

            reader.Read();

            var factorioType = GetFactorioTypeValue(Enum.Parse<PrototypeComplexTypeEnum>(reader.GetString()!, ignoreCase: true), ref reader, options);

            return factorioType;
        }

        private FactorioPrototypeCustomType ReadArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // type
            {
                reader.Read();
            }

            reader.Read();
            var value = ReadType(ref reader, options);

            reader.Read();

            return value;
        }

        private DictionaryPrototypeType ReadDictionary(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // key
            {
                reader.Read();
            }

            reader.Read();

            var key = ReadType(ref reader, options);

            while (reader.TokenType != JsonTokenType.PropertyName) // value
            {
                reader.Read();
            }

            reader.Read();

            var value = ReadType(ref reader, options);

            reader.Read();

            return new DictionaryPrototypeType()
            {
                Key = key!,
                Value = value!
            };
        }

        private FactorioPrototypeCustomType[] ReadTuple(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            List<FactorioPrototypeCustomType> values = [];

            while (reader.TokenType != JsonTokenType.PropertyName) // values
            {
                reader.Read();
            }

            while (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Read();
            }

            reader.Read(); // read one token to be at first element

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                values.Add(ReadType(ref reader, options));
                reader.Read();
            }

            reader.Read();

            return values.ToArray();
        }

        private UnionPrototypeType ReadUnion(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            List<FactorioPrototypeCustomType> unionOptions = [];

            while (reader.TokenType != JsonTokenType.PropertyName) // options
            {
                reader.Read();
            }

            while (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Read();
            }

            reader.Read(); // read one token to be at first element

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                unionOptions.Add(ReadType(ref reader, options));
                reader.Read();
            }

            while (reader.TokenType != JsonTokenType.PropertyName)
            {
                reader.Read();
            }

            reader.Read();

            bool fullFormat = reader.GetBoolean();

            reader.Read();

            return new UnionPrototypeType()
            {
                Options = unionOptions.ToArray(),
                FullFormat = fullFormat!
            };
        }

        private LiteralType ReadLiteral(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // literal
            {
                reader.Read();
            }

            reader.Read();

            object value;

            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    value = reader.GetString() ?? "";
                    break;
                case JsonTokenType.Number:
                    value = reader.GetDouble();
                    break;
                case JsonTokenType.False:
                case JsonTokenType.True:
                    value = reader.GetBoolean();
                    break;
                default:
                    throw new NotSupportedException($"Not supported data type for literal, data type: {reader.TokenType}");
            }

            reader.Read();

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                reader.Read();
                var description = reader.GetString();

                reader.Read();

                return new LiteralType()
                {
                    Value = value!,
                    Description = description!
                };
            }

            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new LiteralType()
                {
                    Value = value!
                };
            }

            throw new JsonException("Literal contains unexpected value.");
        }

        private EmbeddedPrototypeCustomType ReadEmbeddedType(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // type
            {
                reader.Read();
            }

            reader.Read();

            var value = ReadType(ref reader, options);

            while (reader.TokenType != JsonTokenType.PropertyName) // description
            {
                reader.Read();
            }

            reader.Read();

            var description = reader.GetString();

            reader.Read();

            return new EmbeddedPrototypeCustomType()
            {
                Value = value!,
                Description = description!
            };
        }
    }
}
