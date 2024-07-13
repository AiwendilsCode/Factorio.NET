namespace Factorio.Modding.Api.Mod
{
    public class FactorioMod
    {
        public required FactorioModInfo Info { get; set; }
        public List<ModChangelog>? Changelogs { get; set; }
    }
}
