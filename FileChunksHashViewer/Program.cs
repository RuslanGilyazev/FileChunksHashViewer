using System.Diagnostics;
using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Extensions;
using FileChunksHashViewer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileChunksHashViewer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var stopwatch = new Stopwatch(); 
            stopwatch.Start();

            host.Services.GetService<IMainChunksProcessorService>()?.CalculateFileChunksHash();
            
            stopwatch.Stop();
            Console.WriteLine("Elapsed = {0}", stopwatch.Elapsed);
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, builder) => builder
                    .AddRunParametersMemoryCollection(args))
                .ConfigureServices((context, services) => services
                    .AddTransient<IChunksHashProcessorService, ChunksHashProcessorService>()
                    .AddTransient<IMainChunksProcessorService, MainChunksProcessorService>()
                    .AddTransient<IFileHelperService, FileHelperService>()
                    .AddSingleton<ISynchronizationChunkProcessService, SynchronizationChunkProcessService>()
                    .AddScoped<IBufferedChunkReadService, BufferedChunkReadService>()
                    .AddScoped<IChunkHashService, ChunkHashService>()
                    .AddRunParametersConfiguration(context.Configuration)
                    .AddChunkHashOutputService(context.Configuration));
    }
}