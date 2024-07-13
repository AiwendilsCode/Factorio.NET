using Factorio.Modding.Api.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Factorio.Modding.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFactorioModding(this IServiceCollection services)
        {
            services.AddTransient<IModFactory, ModFactory>();
            services.AddTransient<IReaderFactory, ReaderFactory>();
        }
    }
}
