namespace FileChunksHashViewer.Abstraction
{
    public interface IFileHelperService
    {
        public ulong ChunksNumber { get; }
        
        int ReadChunk(Stream threadStream, ulong chunk, byte[] bufferPool);
    }
}