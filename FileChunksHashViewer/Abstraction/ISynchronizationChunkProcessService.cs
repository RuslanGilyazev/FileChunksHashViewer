using FileChunksHashViewer.Services;

namespace FileChunksHashViewer.Abstraction
{
    public interface ISynchronizationChunkProcessService
    {
        ulong GetNextChunk();
        
        SynchronizationChunkBlocker StartChunkCalculationProcess(int threadNumber);
    }
}