using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using FileChunksHashViewer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileChunksHashViewer.Extensions
{
    public static class ChunkHashOutputServiceServicesCollectionExtension
    {
        public static IServiceCollection AddChunkHashOutputService(this IServiceCollection services, IConfiguration configuration)
        {
            var isStrongOrder = configuration.GetSection(nameof(RunParameters))
                .GetValue<bool>(nameof(RunParameters.StrongOrder));
                   
            if (isStrongOrder)
                services.AddSingleton<IChunkHashOutputService, StrongOrderChunkHashOutputService>();
            else
                services.AddSingleton<IChunkHashOutputService, ChunkHashOutputService>();

            return services;
        }
    }
}