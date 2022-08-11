using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class MainChunksProcessorService : IMainChunksProcessorService
    {
        private readonly IFileHelperService _fileHelperService;
        private readonly IServiceProvider _serviceProvider;
        private readonly RunParameters _runParameters;
        
        public MainChunksProcessorService(IFileHelperService fileHelperService, 
            IServiceProvider serviceProvider, 
            IOptions<RunParameters> options)
        {
            _fileHelperService = fileHelperService;
            _serviceProvider = serviceProvider;
            _runParameters = options.Value;
        }
        
        public void CalculateFileChunksHash()
        {
            var chunks = _fileHelperService.ChunksNumber;

            Thread.CurrentThread.Name = "Print thread";
            
            using var scope = _serviceProvider.CreateScope();

            var chunksPerThread = (ulong) Math.Ceiling((decimal) chunks / _runParameters.ThreadsCount) + 1;

            for (var threadNumber = 0; threadNumber < _runParameters.ThreadsCount; threadNumber++)
            {
                var chunkProcessorWorkerService = scope.ServiceProvider.GetRequiredService<IChunksHashProcessorService>();
                chunkProcessorWorkerService.CalculateFileChunksHash(threadNumber, chunks, chunksPerThread).Join();
            }
        }
    }
}