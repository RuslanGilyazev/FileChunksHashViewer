namespace FileChunksHashViewer.Abstraction
{
    public interface IChunkHashService
    {
        string ComputeChunkBase64Hash(byte[] buffer);

        string GenerateChunkHashString(ulong chunk, string hashBase64);
    }
}