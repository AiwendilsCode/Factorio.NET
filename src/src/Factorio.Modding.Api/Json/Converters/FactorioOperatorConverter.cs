using Factorio.Modding.Api.Json.Common;
using Factorio.Modding.Api.Json.Runtime;
using System.Collections.Specialized;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.Modding.Api.Json.Converters
{
    internal class FactorioOperatorConverter : JsonConverter<object[]>
    {
        // contains string and object pairs, to temporary store those values.
        private readonly HybridDictionary _values = [];

        public override object[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<object> operators = [];

            while (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Read();
            }

            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                operators.Add(ReadOperator(ref reader, options));
                reader.Read();
            }

            return operators.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, object[] value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private object ReadOperator(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            ReadPropertiesUntilEndOfObject(ref reader, options);

            if (_values["parameters"] is not null) // method
            {
                return new FactorioMethod()
                {
                    Name = (_values["name"] as string)!,
                    Order = (int)_values["order"]!,
                    Description = (_values["description"] as string)!,
                    Examples = _values["examples"] as string[],
                    Lists = _values["lists"] as string[],
                    Images = _values["images"] as Image[],
                    Format = (_values["format"] as FactorioMethodFormat)!,
                    Parameters = (_values["parameters"] as Parameter[])!,
                    ReturnValues = (_values["return_values"] as Parameter[])!,
                    Raises = _values["raises"] as EventRaised[],
                    VariantParameterGroups = _values["variant_parameter_groups"] as ParameterGroup[],
                    SubClasses = _values["subclasses"] as string[],
                    VariantParameterDescription = _values["variant_parameter_description"] as string,
                    Visibility = _values["visibility"] as string[]
                };
            }

            if (_values["type"] is not null) // attribute
            {
                return new FactorioAttribute()
                {
                    Name = (_values["name"] as string)!,
                    Order = (int)_values["order"]!,
                    Description = (_values["description"] as string)!,
                    Examples = _values["examples"] as string[],
                    Lists = _values["lists"] as string[],
                    Images = _values["images"] as Image[],
                    Visibility = _values["visibility"] as string[],
                    Raises = _values["raises"] as EventRaised[],
                    SubClasses = _values["subclasses"] as string[],
                    Type = (_values["type"] as FactorioRuntimeCustomType)!,
                    Optional = (bool)_values["optional"]!,
                    Read = (bool)_values["read"]!,
                    Write = (bool)_values["write"]!
                };
            }

            throw new NotSupportedException("Not recognized class operator. Check changes in modding api.");
        }

        private void ReadPropertiesUntilEndOfObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var customTypeConverter = options.Converters.Single(converter => converter is FactorioRuntimeCustomTypeConverter)
                as FactorioRuntimeCustomTypeConverter;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    _values[propertyName!] = propertyName switch
                    {
                        "name" or "description" or "variant_parameter_description" => reader.GetString(),
                        "order" => reader.GetInt32(),
                        "lists" or "examples" or "visibility" or "subclasses" => JsonSerializer.Deserialize<string[]>(ref reader, options),
                        "images" => JsonSerializer.Deserialize<Image[]>(ref reader, options),
                        "raises" => JsonSerializer.Deserialize<EventRaised[]>(ref reader, options),
                        "optional" or "read" or "write" => reader.GetBoolean(),
                        "type" => customTypeConverter!.Read(ref reader, typeof(FactorioRuntimeCustomType), options),
                        "parameters" or "return_values" => JsonSerializer.Deserialize<Parameter[]>(ref reader, options),
                        "variant_parameter_groups" => JsonSerializer.Deserialize<ParameterGroup[]>(ref reader, options),
                        "format" => JsonSerializer.Deserialize<FactorioMethodFormat>(ref reader, options),
                        _ => throw new NotSupportedException(
                            $"Property {propertyName} is not valid in a Class operator property. Check changes in modding api."),
                    };
                }
            }
        }
    }
}
