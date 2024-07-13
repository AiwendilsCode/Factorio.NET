namespace Factorio.Modding.Api.Mod
{
    public class ChangelogSection
    {
        public required string Category { get; set; }
        public List<string> Entries { get; set; } = [];
    }
}
