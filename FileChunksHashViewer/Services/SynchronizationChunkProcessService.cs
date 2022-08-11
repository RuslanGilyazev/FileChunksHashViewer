using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class SynchronizationChunkProcessService : ISynchronizationChunkProcessService
    {
        private readonly SpinLock[] _spinLocks;
         
        private long _syncChunk;

        public SynchronizationChunkProcessService(IOptions<RunParameters> options)
        {
            var runParameters = options.Value;
            _spinLocks = new SpinLock[runParameters.ThreadsCount];
            _syncChunk = -1;
        }
        
        public ulong GetNextChunk() => (ulong) Interlocked.Increment(ref _syncChunk);

        public SynchronizationChunkBlocker StartChunkCalculationProcess(int threadNumber) => 
            new (_spinLocks[threadNumber]);
    }
}