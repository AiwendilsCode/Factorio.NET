using Factorio.Modding.Api.Mod;

namespace Factorio.Modding.Api.Factories
{
    public interface IModFactory
    {
        FactorioMod CreateFromZip(string zipPath);
        FactorioMod CreateFromDirectory(string dirPath);
    }
}
