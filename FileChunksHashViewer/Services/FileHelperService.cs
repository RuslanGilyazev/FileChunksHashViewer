using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class FileHelperService : IFileHelperService
    {
        private readonly RunParameters _runParameters;
        
        public ulong ChunksNumber { get; }
        
        public FileHelperService(IOptions<RunParameters> options)
        {
            _runParameters = options.Value;
            
            var fileLength = new FileInfo(_runParameters.FilePath).Length;
            ChunksNumber = (ulong)Math.Ceiling((decimal) fileLength / _runParameters.ChunkSize);
        }

        public int ReadChunk(Stream threadStream, ulong chunk, byte[] buffer)
        {
            threadStream.Seek((long) (chunk * (ulong) _runParameters.ChunkSize), SeekOrigin.Begin);

            try
            {
                var readBytes = threadStream.Read(buffer, 0, buffer.Length);
                return readBytes;
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                Console.WriteLine("Задан слишком большой размер чанков для обработки. " +
                                  "Уменьшите размер чанков или количество процессов");
                Console.WriteLine(outOfMemoryException);
                throw;
            }
        }
    }
}