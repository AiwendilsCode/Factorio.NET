using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Factorio.Modding.Api.Mod
{
    public class ModDependency : IParsable<ModDependency>
    {
        public required string Name { get; set; }
        public required DependencyKind Kind { get; set; }
        public Version? Version { get; set; }
        public EqualityOperator? EqualityOperator { get; set; }

        public static ModDependency Parse(string s, IFormatProvider? provider)
        {
            if (TryParse(s, null, out var modDependency))
            {
                return modDependency;
            }

            throw new ArgumentException($"Cannot parse string {s} to {nameof(ModDependency)}. Check {nameof(modDependency)} format here https://wiki.factorio.com/Tutorial:Mod_structure#Dependency.");
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ModDependency result)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                result = null;
                return false;
            }

            var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length > 4)
            {
                result = null;
                return false;
            }

            EqualityOperator? equalityOperator = null;
            Version? modVersion = null;
            DependencyKind dependencyKind = DependencyKind.Hard;
            string dependencyName = String.Empty;

            switch (parts.Length)
            {
                case 1:
                    if (DetermineEqualityOperator(parts[0]) is not null || 
                        DetermineDependencyKind(parts[0]) is not DependencyKind.Hard ||
                        Version.TryParse(parts[0], out _))
                    {
                        result = null;
                        return false;
                    }

                    dependencyKind = DependencyKind.Hard;
                    dependencyName = parts[0];
                    break;
                case 2:
                    if (DetermineEqualityOperator(parts[0]) is not null ||
                        DetermineEqualityOperator(parts[1]) is not null ||
                        Version.TryParse(parts[0], out _) ||
                        Version.TryParse(parts[1], out _))
                    {
                        result = null;
                        return false;
                    }

                    dependencyKind = DetermineDependencyKind(parts[0]);
                    dependencyName = parts[1];
                    break;
                case 3:
                    if (DetermineDependencyKind(parts[0]) is not DependencyKind.Hard)
                    {
                        result = null;
                        return false;
                    }

                    equalityOperator = DetermineEqualityOperator(parts[1]);
                    if (equalityOperator is null)
                    {
                        result = null;
                        return false;
                    }

                    if (!Version.TryParse(parts[2], out modVersion))
                    {
                        result = null;
                        return false;
                    }

                    dependencyName = parts[0];
                    dependencyKind = DependencyKind.Hard;
                    break;
                case 4:
                    dependencyKind = DetermineDependencyKind(parts[0]);
                    if (dependencyKind is DependencyKind.Hard)
                    {
                        result = null;
                        return false;
                    }
                    
                    equalityOperator = DetermineEqualityOperator(parts[2]);
                    if (equalityOperator is null)
                    {
                        result = null;
                        return false;
                    }

                    if (!Version.TryParse(parts[3], out modVersion))
                    {
                        result = null;
                        return false;
                    }

                    dependencyName = parts[1];
                    break;
            }

            result = new ModDependency
            {
                Name = dependencyName,
                Kind = dependencyKind,
                Version = modVersion,
                EqualityOperator = equalityOperator
            };
            return true;
        }

        public override string ToString()
        {
            var dependencyBuilder = new StringBuilder();
            dependencyBuilder.Append(GetKindAsString());
            dependencyBuilder.Append(Name);
            dependencyBuilder.Append(GetEqOperatorAsString());
            dependencyBuilder.Append(Version?.ToString() ?? "");

            return dependencyBuilder.ToString();
        }

        private static DependencyKind DetermineDependencyKind(string prefix)
        {
            return prefix switch
            {
                "!" => DependencyKind.Incompatible,
                "?" => DependencyKind.Optional,
                "(?)" => DependencyKind.HiddenOptional,
                "~" => DependencyKind.NotAffectLoadOrder,
                _ => DependencyKind.Hard
            };
        }

        private static EqualityOperator? DetermineEqualityOperator(string prefix)
        {
            return prefix switch
            {
                "<" => Mod.EqualityOperator.Less,
                "<=" => Mod.EqualityOperator.LessOrEqual,
                "=" => Mod.EqualityOperator.Equal,
                ">" => Mod.EqualityOperator.Higher,
                ">=" => Mod.EqualityOperator.HigherOrEqual,
                _ => null
            };
        }

        private string GetKindAsString()
        {
            return Kind switch
            {
                DependencyKind.Hard => "",
                DependencyKind.HiddenOptional => "(?) ",
                DependencyKind.Incompatible => "! ",
                DependencyKind.NotAffectLoadOrder => "~ ",
                DependencyKind.Optional => "? ",
                _ => throw new ArgumentException("Invalid dependency kind.")
            };
        }

        private string GetEqOperatorAsString()
        {
            return EqualityOperator switch
            {
                Mod.EqualityOperator.Less => " < ",
                Mod.EqualityOperator.LessOrEqual => " <= ",
                Mod.EqualityOperator.Equal => " = ",
                Mod.EqualityOperator.Higher => " > ",
                Mod.EqualityOperator.HigherOrEqual => " >= ",
                null => "",
                _ => throw new ArgumentException("Invalid Equality operator.")
            };
        }
    }
}
