using System.IO;
using Microsoft.Extensions.Logging;

namespace LoggingKata.Services
{
    //service responsible for reading CSV files
    //separates file I/O concerns from business logic
    public class CsvFileReaderService
    {
        private readonly ILogger<CsvFileReaderService> _logger;

        public CsvFileReaderService(ILogger<CsvFileReaderService> logger)
        {
            _logger = logger;
        }

        //reads all lines from a CSV file and returns them as a string array
        public string[] ReadAllLines(string filePath)
        {
            _logger.LogInformation($"Reading CSV file: {filePath}");
            
            if (!File.Exists(filePath))
            {
                _logger.LogError($"File not found: {filePath}");
                throw new FileNotFoundException($"CSV file not found: {filePath}");
            }

            var lines = File.ReadAllLines(filePath);
            _logger.LogInformation($"Successfully read {lines.Length} lines from {filePath}");
            
            return lines;
        }
    }
}

