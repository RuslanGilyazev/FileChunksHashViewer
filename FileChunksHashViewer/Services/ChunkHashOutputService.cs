using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class ChunkHashOutputService : IChunkHashOutputService
    {
        private readonly ISynchronizationChunkProcessService _synchronizationChunkProcessService;
        
        protected readonly RunParameters RunParameters;

        public ChunkHashOutputService(ISynchronizationChunkProcessService synchronizationChunkProcessService,
            IOptions<RunParameters> options)
        {
            _synchronizationChunkProcessService = synchronizationChunkProcessService;
            RunParameters = options.Value;
        }

        public virtual void Print(string result)
        {
            Console.WriteLine(result);
        }

        public virtual void BlockingPrint(int threadNumber, ulong chunk, string result)
        {
            var prevThreadNumber = threadNumber - 1;
            if (prevThreadNumber < 0) prevThreadNumber = RunParameters.ThreadsCount - 1;

            using (_synchronizationChunkProcessService.StartChunkCalculationProcess(prevThreadNumber))
            {
                PreparationToPrint(chunk);
                Print(result);
            }
        }

        protected virtual void PreparationToPrint(ulong chunk)
        {
        }
    }
}