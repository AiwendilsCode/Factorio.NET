using System.Runtime.Serialization;

namespace Factorio.Modding.Api.Json.Prototypes
{
    public enum PrototypeComplexTypeEnum
    {
        [EnumMember(Value = "array")]
        Array,
        [EnumMember(Value = "dictionary")]
        Dictionary,
        [EnumMember(Value = "tuple")]
        Tuple,
        [EnumMember(Value = "union")]
        Union,
        [EnumMember(Value = "literal")]
        Literal,
        [EnumMember(Value = "type")]
        Type,
        [EnumMember(Value = "struct")]
        Struct
    }
}
