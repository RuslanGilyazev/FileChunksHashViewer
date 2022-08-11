namespace FileChunksHashViewer.Abstraction
{
    public interface IBufferedChunkReadService
    {
        int ReadChunkBytes(Stream stream, ulong chunk, out byte[] buffer);

        void ReturnBuffer(byte[] buffer);
    }
}