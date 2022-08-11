namespace FileChunksHashViewer.Abstraction
{
    public interface IChunksHashProcessorService
    {
        Thread CalculateFileChunksHash(int threadNumber, ulong chunks, ulong chunksPerThread);
    }
}