using FileChunksHashViewer.Models;
using Microsoft.Extensions.Configuration;

namespace FileChunksHashViewer.Extensions
{
    public static class RunParametersConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddRunParametersMemoryCollection(this IConfigurationBuilder builder, string[] args)
        {
            builder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
                new($"{nameof(RunParameters)}:{nameof(RunParameters.FilePath)}", 
                    args is {Length: >= 1} ? args[0] : null),
                new($"{nameof(RunParameters)}:{nameof(RunParameters.ChunkSize)}", 
                    args is {Length: >= 2} ? args[1] : null),
                new($"{nameof(RunParameters)}:{nameof(RunParameters.StrongOrder)}", 
                    args is {Length: >= 3} ? args[2] : null),
                // Один поток нужен для вывода
                new($"{nameof(RunParameters)}:{nameof(RunParameters.ThreadsCount)}", 
                    args is {Length: >= 4} ? args[3] : (Environment.ProcessorCount - 1).ToString()),
            });

            return builder;
        }
    }
}