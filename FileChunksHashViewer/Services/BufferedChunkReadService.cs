using System.Buffers;
using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class BufferedChunkReadService : IBufferedChunkReadService
    {
        private readonly IFileHelperService _fileHelperService;
        private readonly RunParameters _runParameters;
        private readonly ArrayPool<byte> _buffetPool;

        public BufferedChunkReadService(IFileHelperService fileHelperService,
            IOptions<RunParameters> options)
        {
            _fileHelperService = fileHelperService;
            _runParameters = options.Value;
            
            _buffetPool = ArrayPool<byte>.Create(_runParameters.ChunkSize,
                _runParameters.ChunkSize);
        }
        
        public int ReadChunkBytes(Stream stream, ulong chunk, out byte[] buffer)
        {
            buffer = _buffetPool.Rent(_runParameters.ChunkSize);
            var readBytes = _fileHelperService.ReadChunk(stream, chunk, buffer);
            
            return readBytes;
        }

        public void ReturnBuffer(byte[] buffer) => _buffetPool.Return(buffer);
    }
}