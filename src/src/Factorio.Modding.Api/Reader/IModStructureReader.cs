using Factorio.Modding.Api.Mod;

namespace Factorio.Modding.Api.Reader
{
    public interface IModStructureReader : IDisposable
    {
        FactorioModInfo ReadInfo();
        ModChangelog[]? ReadChangelogs();
    }
}
