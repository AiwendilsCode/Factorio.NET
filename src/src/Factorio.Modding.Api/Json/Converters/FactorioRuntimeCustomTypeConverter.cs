using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Converters
{
    // In every read method I do one additional reader.Read()
    // This is because, in ReadLiteral method it is required,
    // thus I am doing it everywhere else, to avoid
    // some sketchy if statements or tricks.
    internal class FactorioRuntimeCustomTypeConverter : JsonConverter<FactorioRuntimeCustomType>
    {
        public override FactorioRuntimeCustomType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = ReadType(ref reader, options);

            return result;
        }

        public override void Write(Utf8JsonWriter writer, FactorioRuntimeCustomType value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private FactorioRuntimeCustomType GetFactorioTypeValue(RuntimeComplexTypeEnum complexType, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            object? value = complexType switch
            {
                RuntimeComplexTypeEnum.Type => ReadEmbeddedType(ref reader, options),
                RuntimeComplexTypeEnum.Union => ReadUnion(ref reader, options),
                RuntimeComplexTypeEnum.Array => ReadArray(ref reader, options),
                RuntimeComplexTypeEnum.Dictionary => ReadDictionary(ref reader, options),
                RuntimeComplexTypeEnum.LuaCustomTable => ReadDictionary(ref reader, options),
                RuntimeComplexTypeEnum.Table => ReadTable(ref reader, options),
                RuntimeComplexTypeEnum.Tuple => ReadTuple(ref reader, options),
                RuntimeComplexTypeEnum.Function => ReadFunction(ref reader, options),
                RuntimeComplexTypeEnum.Literal => ReadLiteral(ref reader, options),
                RuntimeComplexTypeEnum.LuaLazyLoadedValue => ReadLuaLazyLoadedValue(ref reader, options),
                RuntimeComplexTypeEnum.LuaStruct => ReadLuaStruct(ref reader, options),
                RuntimeComplexTypeEnum.BuiltIn => ReadBuiltIn(ref reader),
                _ => throw new NotSupportedException($"Not supported complex type: {complexType}."),
            };

            return new FactorioRuntimeCustomType { ComplexType = complexType, Value = value };
        }

        private object? ReadBuiltIn(ref Utf8JsonReader reader)
        {
            reader.Read();
            return null;
        }

        private FactorioRuntimeCustomType ReadType(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var strValue = reader.GetString();

                return new FactorioRuntimeCustomType
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

            var factorioType = GetFactorioTypeValue(Enum.Parse<RuntimeComplexTypeEnum>(reader.GetString()!, ignoreCase: true), ref reader, options);

            return factorioType;
        }

        private FactorioRuntimeCustomType ReadArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
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

        private DictionaryRuntimeType ReadDictionary(ref Utf8JsonReader reader, JsonSerializerOptions options)
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

            return new DictionaryRuntimeType
            {
                Key = key!,
                Value = value!
            };
        }

        private TupleRuntimeType ReadTuple(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            List<FactorioRuntimeCustomType> values = [];

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

            return new TupleRuntimeType { Values = values.ToArray() };
        }

        private UnionRuntimeType ReadUnion(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            List<FactorioRuntimeCustomType> unionOptions = [];

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

            return new UnionRuntimeType()
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

        private EmbeddedRuntimeCustomType ReadEmbeddedType(ref Utf8JsonReader reader, JsonSerializerOptions options)
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

            return new EmbeddedRuntimeCustomType
            {
                Value = value!,
                Description = description!
            };
        }

        private TableType ReadTable(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // parameters
            {
                reader.Read();
            }

            reader.Read();
            var parameters = JsonSerializer.Deserialize<Parameter[]>(ref reader, options);

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                return new TableType()
                {
                    Parameters = parameters!
                };
            }

            ParameterGroup[]? variantParameterGroups;

            reader.Read();
            variantParameterGroups = JsonSerializer.Deserialize<ParameterGroup[]>(ref reader, options);

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                return new TableType()
                {
                    Parameters = parameters!,
                    VariantParameterGroups = variantParameterGroups
                };
            }

            reader.Read();
            var variantParameterDecription = reader.GetString();

            reader.Read();

            return new TableType()
            {
                Parameters = parameters!,
                VariantParameterGroups = variantParameterGroups!,
                VariantParameterDescription = variantParameterDecription!
            };
        }

        private FunctionType ReadFunction(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            List<FactorioRuntimeCustomType> values = [];

            while (reader.TokenType != JsonTokenType.PropertyName) // parameters
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

            return new FunctionType { Parameters = values.ToArray() };
        }

        private LuaLazyLoadedValueType ReadLuaLazyLoadedValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // value
            {
                reader.Read();
            }

            reader.Read();
            var type = ReadType(ref reader, options);

            reader.Read();

            return new LuaLazyLoadedValueType { Value = type };
        }

        private LuaStructType ReadLuaStruct(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.TokenType != JsonTokenType.PropertyName) // attributes
            {
                reader.Read();
            }

            reader.Read();
            var attributes = JsonSerializer.Deserialize<FactorioAttribute[]>(ref reader, options);

            reader.Read();

            return new LuaStructType { Attributes = attributes! };
        }
    }
}
