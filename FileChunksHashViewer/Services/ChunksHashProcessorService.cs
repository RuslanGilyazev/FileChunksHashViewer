using FileChunksHashViewer.Abstraction;
using FileChunksHashViewer.Models;
using Microsoft.Extensions.Options;

namespace FileChunksHashViewer.Services
{
    public class ChunksHashProcessorService : IChunksHashProcessorService
    {
        private readonly IBufferedChunkReadService _bufferedChunkReadService;
        private readonly IChunkHashService _chunkHashService;
        private readonly ISynchronizationChunkProcessService _synchronizationChunkProcessService;
        private readonly IChunkHashOutputService _chunkHashOutputService;
        private readonly RunParameters _runParameters;
        
        public ChunksHashProcessorService(
            IBufferedChunkReadService bufferedChunkReadService,
            IChunkHashService chunkHashService,
            ISynchronizationChunkProcessService synchronizationChunkProcessService, 
            IChunkHashOutputService chunkHashOutputService,
            IOptions<RunParameters> options)
        {
            _bufferedChunkReadService = bufferedChunkReadService;
            _chunkHashService = chunkHashService;
            _synchronizationChunkProcessService = synchronizationChunkProcessService;
            _chunkHashOutputService = chunkHashOutputService;
            _runParameters = options.Value;
        }
        
        public Thread CalculateFileChunksHash(int threadNumber, ulong chunks, ulong chunksPerThread)
        {
            var thread = new Thread(() =>
            {
                using Stream threadStream = new FileStream(_runParameters.FilePath, FileMode.Open,
                    FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess);

                for (ulong threadWorkId = 0; threadWorkId < chunksPerThread; threadWorkId++)
                {
                    ulong chunk;
                    string hashLineString;
                    
                    using (_synchronizationChunkProcessService.StartChunkCalculationProcess(threadNumber))
                    {
                        chunk = _synchronizationChunkProcessService.GetNextChunk();
                        var readBytes = _bufferedChunkReadService.ReadChunkBytes(threadStream, chunk, out var buffer);

                        if (readBytes == 0)
                        {
                            return;
                        }
                    
                        var hashBase64 = _chunkHashService.ComputeChunkBase64Hash(buffer);
                        _bufferedChunkReadService.ReturnBuffer(buffer);
                        hashLineString = _chunkHashService.GenerateChunkHashString(chunk, hashBase64);
                    }
                    
                    if (chunk == 0)
                    {
                        _chunkHashOutputService.Print(hashLineString);
                        continue;
                    }

                    _chunkHashOutputService.BlockingPrint(threadNumber, chunk, hashLineString);
                }

                threadStream.Close();
            })
            {
                IsBackground = true,
                Name = $"Chunk processer {threadNumber}"
            };

            thread.Start();

            return thread;
        }
    }
}