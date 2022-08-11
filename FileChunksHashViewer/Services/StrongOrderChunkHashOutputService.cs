using System.Collections.Concurrent;
using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class StrongOrderChunkHashOutputService : ChunkHashOutputService
    {
        private readonly ConcurrentQueue<ulong> _pintQueue;
        
        public StrongOrderChunkHashOutputService(ISynchronizationChunkProcessService synchronizationChunkProcessService, 
            IFileHelperService fileHelperService, 
            IOptions<RunParameters> options) : base(synchronizationChunkProcessService, options)
        {
            _pintQueue = new ConcurrentQueue<ulong>();
            
            for (ulong i = 0; i < fileHelperService.ChunksNumber; i++)
            {
                _pintQueue.Enqueue(i);
            }
        }

        protected override void PreparationToPrint(ulong chunk)
        {
            base.PreparationToPrint(chunk);

            _pintQueue.TryPeek(out var nextToPrintElement);
            while (nextToPrintElement != chunk)
            {
                _pintQueue.TryPeek(out var tempNextToPrintElement);
                nextToPrintElement = tempNextToPrintElement;
            }
        }

        public override void Print(string result)
        {
            base.Print(result);
            

            if (RunParameters.StrongOrder)
            {
                _pintQueue.TryDequeue(out _);
            }
        }
    }
}