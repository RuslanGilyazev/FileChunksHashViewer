namespace FileChunksHashViewer.Models
{
    public class RunParameters
    {
        public string FilePath { get; set; }
        
        public int ChunkSize { get; set; }
        
        public bool StrongOrder { get; set; }

        // Один поток нужен для вывода
        public int ThreadsCount { get; set; } = Environment.ProcessorCount - 1;
    }
}