namespace FileChunksHashViewer.Abstraction
{
    public interface IChunkHashOutputService
    {
        void Print(string result);
        
        void BlockingPrint(int threadNumber, ulong chunk, string result);
    }
}