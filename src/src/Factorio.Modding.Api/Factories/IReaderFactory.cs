using Factorio.Modding.Api.Reader;

namespace Factorio.Modding.Api.Factories
{
    public interface IReaderFactory
    {
        IModStructureReader CreateReaderForDirectory(string path);
    }
}
