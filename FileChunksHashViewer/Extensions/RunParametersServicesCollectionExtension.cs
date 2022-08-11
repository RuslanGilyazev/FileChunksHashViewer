using FileChunksHashViewer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileChunksHashViewer.Extensions
{
    public static class RunParametersServicesCollectionExtension
    {
        public static IServiceCollection AddRunParametersConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<RunParameters>()
                .Bind(configuration.GetSection(nameof(RunParameters)))
                .Validate(parameters => !string.IsNullOrEmpty(parameters.FilePath), "Не задан путь к файлу")
                .Validate(parameters => File.Exists(parameters.FilePath), "Отсутсвтует файл")
                .Validate(parameters => parameters.ChunkSize > 0, "Не задан размер чанка");

            return services;
        }
    }
}