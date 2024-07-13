using Factorio.Modding.Api.Mod;

namespace Factorio.Modding.Api.Factories
{
    internal class ModFactory(IReaderFactory readerFactory) : IModFactory
    {
        private readonly IReaderFactory _readerFactory = readerFactory;

        public FactorioMod CreateFromDirectory(string dirPath)
        {
            using (var reader = _readerFactory.CreateReaderForDirectory(dirPath))
            {
                return new FactorioMod
                {
                    Info = reader.ReadInfo(),
                    Changelogs = reader.ReadChangelogs()?.ToList()
                };
            }
        }

        public FactorioMod CreateFromZip(string zipPath)
        {
            throw new NotImplementedException();
        }
    }
}
