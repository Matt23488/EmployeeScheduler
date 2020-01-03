using EmployeeScheduler.Lib.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class JsonFileService : IJsonFileService
    {
        public async Task<T> ReadJsonAsync<T>(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);
            using var reader = new StreamReader(fileStream);

            var fileContents = await reader.ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(fileContents);
        }

        public async Task WriteJsonAsync(string filePath, object data)
        {
            using var writer = new StreamWriter(filePath, false);

            var fileContents = JsonConvert.SerializeObject(data);

            await writer.WriteAsync(fileContents);
        }

        public T ReadJson<T>(string filePath)
            => JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));

        public void WriteJson(string filePath, object data)
            => File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
    }
}
