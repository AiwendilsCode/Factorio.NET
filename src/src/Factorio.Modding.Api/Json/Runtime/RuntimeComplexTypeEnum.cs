using System.Runtime.Serialization;

namespace Factorio.Modding.Api.Json.Runtime
{
    public enum RuntimeComplexTypeEnum
    {
        [EnumMember(Value = "type")]
        Type,
        [EnumMember(Value = "union")]
        Union,
        [EnumMember(Value = "array")]
        Array,
        [EnumMember(Value = "dictionary")]
        Dictionary,
        [EnumMember(Value = "LuaCustomTable")]
        LuaCustomTable,
        [EnumMember(Value = "table")]
        Table,
        [EnumMember(Value = "tuple")]
        Tuple,
        [EnumMember(Value = "function")]
        Function,
        [EnumMember(Value = "literal")]
        Literal,
        [EnumMember(Value = "LuaLazyLoadedValue")]
        LuaLazyLoadedValue,
        [EnumMember(Value = "LuaStruct")]
        LuaStruct,
        /// <summary>
        /// This value can occur only in Concept.
        /// </summary>
        [EnumMember(Value = "builtin")]
        BuiltIn
    }
}
