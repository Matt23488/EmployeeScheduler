using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IJsonFileService
    {
        Task<T> ReadJsonAsync<T>(string filePath);
        Task WriteJsonAsync(string filePath, object data);

        T ReadJson<T>(string filePath);
        void WriteJson(string filePath, object data);
    }
}
