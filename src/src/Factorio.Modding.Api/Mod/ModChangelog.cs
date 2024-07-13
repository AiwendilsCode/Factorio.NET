using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Factorio.Modding.Api.Mod
{
    public class ModChangelog : IParsable<ModChangelog>
    {
        public required Version Version { get; set; }
        public DateOnly? Date { get; set; }
        public List<ChangelogSection>? Sections { get; set; }

        /// <summary>
        /// Used for delimiting changelogs, not values in them.
        /// </summary>
        public static string Delimiter => "---------------------------------------------------------------------------------------------------";

        public static ModChangelog Parse(string s, IFormatProvider? provider)
        {
            if (TryParse(s, null, out var modChangelog))
            {
                return modChangelog;
            }

            throw new ArgumentException($"Cannot parse string {s} to {nameof(ModChangelog)}. Check {nameof(ModChangelog)} format here https://wiki.factorio.com/Tutorial:Mod_changelog_format.");
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ModChangelog result)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                result = null;
                return false;
            }

            string[] parts = s.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0 || 
                !parts[0].StartsWith("Version: ") || 
                !Version.TryParse(parts[0].Replace("Version: ", ""), out var changelogVersion))
            {
                result = null;
                return false;
            }

            DateOnly changelogDate = DateOnly.MinValue;
            List<ChangelogSection>? changelogSections = [];

            if (parts.Length > 1)
            {
                if (parts[1].StartsWith("Date: "))
                {
                    if (!DateOnly.TryParseExact(parts[1].Replace("Date: ", ""), "dd. MM. yyyy", out changelogDate))
                    {
                        result = null;
                        return false;
                    }

                    if (parts.Length > 2)
                    {
                        changelogSections = GetSections(parts[2..]);
                    }
                }
                else
                {
                    changelogSections = GetSections(parts[1..]);
                }

                if (changelogSections is null)
                {
                    result = null;
                    return false;
                }
            }

            result = new ModChangelog
            {
                Version = changelogVersion,
                Date = changelogDate == DateOnly.MinValue ? null : changelogDate,
                Sections = changelogSections
            };
            return true;
        }

        public override string ToString()
        {
            var changelogBuilder = new StringBuilder();

            changelogBuilder.Append($"Version: {Version}");

            if (Date is not null)
            {
                changelogBuilder.AppendLine();
                changelogBuilder.Append($"Date: {Date.Value.ToString("dd. MM. yyyy")}");
            }

            if (Sections is not null && Sections.Count != 0)
            {
                changelogBuilder.AppendLine();

                foreach (var section in Sections)
                {
                    changelogBuilder.Append($"  {section.Category}");

                    if (Sections.Count != 0)
                    {
                        changelogBuilder.AppendLine();

                        foreach (var entry in section.Entries)
                        {
                            var parts = entry.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                            changelogBuilder.Append($"    - {parts[0]}");

                            for (int i = 1; i < parts.Length; i++)
                            {
                                changelogBuilder.AppendLine();
                                changelogBuilder.Append($"      {parts[i]}");
                            }
                        }
                    }
                }
            }

            return changelogBuilder.ToString();
        }

        private static List<ChangelogSection>? GetSections(string[] parts)
        {
            List<ChangelogSection> sections = [];

            foreach (var part in parts)
            {
                if (part.StartsWith("  ") && part.EndsWith(':'))
                {
                    sections.Add(new ChangelogSection
                    {
                        Category = part.Trim().TrimEnd(':'),
                        Entries = []
                    });
                }
                else if (part.StartsWith("    - "))
                {
                    sections[^1].Entries.Add(part[6..].TrimEnd());
                }
                else if (part.StartsWith("      "))
                {
                    sections[^1].Entries[^1] += Environment.NewLine + part.Trim();
                }
                else
                {
                    return null;
                }
            }

            return sections;
        }
    }
}
