using System.Security.Cryptography;
using FileChunksHashViewer.Abstraction;

namespace FileChunksHashViewer.Services
{
    public class ChunkHashService : IChunkHashService
    {
        private readonly SHA256 _sha256;

        public ChunkHashService() => _sha256 = SHA256.Create();

        public string ComputeChunkBase64Hash(byte[] buffer)
        {
            var hash = _sha256.ComputeHash(buffer);
            return Convert.ToBase64String(hash);
        }
        
        public string GenerateChunkHashString(ulong chunk, string hashBase64) => $"{chunk}: {hashBase64}";
    }
}