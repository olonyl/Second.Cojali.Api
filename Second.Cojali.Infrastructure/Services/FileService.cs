using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Cojali.Infrastructure.Services
{
    public static class FileService
    {
        public static async Task<string> ReadFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found at {filePath}");

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task WriteFileAsync(string filePath, string content)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                await writer.WriteAsync(content);
            }
        }

    }
}
