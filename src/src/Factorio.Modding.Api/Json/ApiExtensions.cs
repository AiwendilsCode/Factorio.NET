using Factorio.Modding.Api.Json.Prototypes;
using Factorio.Modding.Api.Json.Runtime;

namespace Factorio.Modding.Api.Json
{
    public static class ApiExtensions
    {
        public static void Order(this PrototypeApi api)
        {
            Array.Sort(api.Prototypes, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var prototype in api.Prototypes)
            {
                Array.Sort(prototype.Properties, 
                    (first, second) => first.Order > second.Order ? 1 : -1);
            }

            Array.Sort(api.Types, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var type in api.Types)
            {
                if (type.Properties is not null)
                {
                    Array.Sort(type.Properties, (first, second) => first.Order > second.Order ? 1 : -1);
                }
            }


            api.IsOrdered = true;
        }

        public static void Order(this RuntimeApi api)
        {
            OrderClasses(api.Classes);
            Array.Sort(api.Concepts, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var concept in api.Concepts)
            {
                OrderRuntimeCustomType(concept.Type);
            }

            OrderDefines(api.Defines);

            Array.Sort(api.Events, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var factorioEvent in api.Events)
            {
                Array.Sort(factorioEvent.Data, (first, second) => first.Order > second.Order ? 1 : -1);
                foreach (var parameter in factorioEvent.Data)
                {
                    OrderRuntimeCustomType(parameter.Type);
                }
            }

            OrderMethods(api.GlobalFunctions);
            Array.Sort(api.GlobalObjects, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var globalObject in api.GlobalObjects)
            {
                OrderRuntimeCustomType(globalObject.Type);
            }

            api.IsOrdered = true;
        }

        private static void OrderClasses(FactorioClass[] classes)
        {
            Array.Sort(classes, (first, second) => first.Order > second.Order ? 1 : -1);

            foreach (var factorioClass in classes)
            {
                Array.Sort(factorioClass.Attributes, (first, second) => first.Order > second.Order ? 1 : -1);
                foreach (var attribute in factorioClass.Attributes)
                {
                    OrderRuntimeCustomType(attribute.Type);
                    if (attribute.Raises is not null)
                    {
                        Array.Sort(attribute.Raises, (first, second) => first.Order > second.Order ? 1 : -1);
                    }
                }

                OrderMethods(factorioClass.Methods);
            }
        }

        private static void OrderDefines(Define[] defines)
        {
            Array.Sort(defines, (first, second) => first.Order > second.Order ? 1 : -1);

            foreach (Define define in defines)
            {
                if (define.Values is not null)
                {
                    Array.Sort(define.Values, (first, second) => first.Order > second.Order ? 1 : -1);
                }

                if (define.SubKeys is not null)
                {
                    OrderDefines(define.SubKeys);
                }
            }
        }

        private static void OrderMethods(FactorioMethod[] methods)
        {
            Array.Sort(methods, (first, second) => first.Order > second.Order ? 1 : -1);
            foreach (var method in methods)
            {
                if (method.Raises is not null)
                {
                    Array.Sort(method.Raises, (first, second) => first.Order > second.Order ? 1 : -1);
                }

                if (method.VariantParameterGroups is not null)
                {
                    Array.Sort(method.VariantParameterGroups, (first, second) => first.Order > second.Order ? 1 : -1);
                    foreach (var variadicPrameterGroup in method.VariantParameterGroups)
                    {
                        Array.Sort(variadicPrameterGroup.Parameters, (first, second) => first.Order > second.Order ? 1 : -1);
                        foreach (var parameter in variadicPrameterGroup.Parameters)
                        {
                            OrderRuntimeCustomType(parameter.Type);
                        }
                    }
                }

                Array.Sort(method.Parameters, (first, second) => first.Order > second.Order ? 1 : -1);
                foreach (var parameter in method.Parameters)
                {
                    OrderRuntimeCustomType(parameter.Type);
                }

                Array.Sort(method.ReturnValues, (first, second) => first.Order > second.Order ? 1 : -1);
                foreach (var returnValue in method.ReturnValues)
                {
                    OrderRuntimeCustomType(returnValue.Type);
                }
            }
        }

        private static void OrderRuntimeCustomType(FactorioRuntimeCustomType type)
        {
            switch (type.Value)
            {
                case TableType table:
                    Array.Sort(table.Parameters, (first, second) => first.Order > second.Order ? 1 : -1);
                    if (table.VariantParameterGroups is not null)
                    {
                        foreach (var variantParameterGroup in table.VariantParameterGroups)
                        {
                            Array.Sort(variantParameterGroup.Parameters, (first, second) => first.Order > second.Order ? 1 : -1);
                        }
                    }
                    break;
                case EmbeddedRuntimeCustomType embeddedType:
                    OrderRuntimeCustomType(embeddedType.Value);
                    break;
                case UnionRuntimeType union:
                    foreach (var unionType in union.Options)
                    {
                        OrderRuntimeCustomType(unionType);
                    }
                    break;
                case ArrayRuntimeType array: // array
                    OrderRuntimeCustomType(array.Value);
                    break;
                case DictionaryRuntimeType dictionary:
                    OrderRuntimeCustomType(dictionary.Value);
                    OrderRuntimeCustomType(dictionary.Key);
                    break;
                case TupleRuntimeType tuple:
                    foreach (var tupleValue in tuple.Values)
                    {
                        OrderRuntimeCustomType(tupleValue);
                    }
                    break;
                case FunctionType function:
                    foreach (var parameter in function.Parameters)
                    {
                        OrderRuntimeCustomType(parameter);
                    }
                    break;
                case LuaLazyLoadedValueType lazyValue:
                    OrderRuntimeCustomType(lazyValue.Value);
                    break;
                case LuaStructType luaStruct:
                    foreach (var attribute in luaStruct.Attributes)
                    {
                        OrderRuntimeCustomType(attribute.Type);
                        if (attribute.Raises is not null)
                        {
                            Array.Sort(attribute.Raises, (first, second) => first.Order > second.Order ? 1 : -1);
                        }
                    }
                    break;
            }
        }
    }
}
