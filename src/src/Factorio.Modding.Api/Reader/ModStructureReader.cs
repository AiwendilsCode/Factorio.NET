using System.Text.Json;
using Factorio.Modding.Api.Mod;

namespace Factorio.Modding.Api.Reader
{
    internal class ModStructureReader : IModStructureReader
    {
        private readonly string _dirPath;

        public ModStructureReader(string directoryPath)
        {
            _dirPath = directoryPath;
        }

        public FactorioModInfo ReadInfo()
        {
            var file = Path.Combine(_dirPath, "info.json");

            if (!File.Exists(file))
            {
                throw new Exception("Invalid mod, file info.json does not exist.");
            }

            try
            {
                return JsonSerializer.Deserialize<FactorioModInfo>(file)!;
            }
            catch (Exception e)
            {
                throw new Exception("File info.json is not valid.", e);
            }
        }

        public ModChangelog[]? ReadChangelogs()
        {
            var file = Path.Combine(_dirPath, "changelog.txt");
            if (!File.Exists(file))
            {
                return null;
            }

            string[] changelogsInStr = File.ReadAllText(file).Split(ModChangelog.Delimiter,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                
            var changelogs = new ModChangelog[changelogsInStr.Length];

            for (int i = 0; i < changelogs.Length; i++)
            {
                changelogs[i] = ModChangelog.Parse(changelogsInStr[i], null);
            }

            return changelogs;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}
