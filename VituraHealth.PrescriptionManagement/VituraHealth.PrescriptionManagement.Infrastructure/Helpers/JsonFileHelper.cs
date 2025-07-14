using System.Text.Json;

/// <summary>
/// Helper class to read and write FJSON files and de/serialize JSON data.
/// </summary>
namespace VituraHealth.PrescriptionManagement.Infrastructure.Helpers
{
    public static class JsonFileHelper
    {
        public static List<T> ReadFromJsonFile<T>(string fileName)
        {
            string jsonString = File.ReadAllText(Path.Combine($"{Directory.GetCurrentDirectory()}\\Data", fileName));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(jsonString, options);
        }

        public static void WriteToJsonFile<T>(List<T> data, string fileName)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, jsonString);
        }
    }
}