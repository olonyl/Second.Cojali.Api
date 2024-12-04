using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Cojali.Infrastructure.Services
{
    public static class FileService
    {
        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found at {filePath}");

            return File.ReadAllText(filePath);
        }

        public static void WriteFile(string filePath, string content) => File.WriteAllText(filePath, content);

    }
}
