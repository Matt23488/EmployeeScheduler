using EmployeeScheduler.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IFetchService
    {
        Task<FetchResult<T>> GetAsync<T>(string url);
        Task<FetchResult<T>> GetAsync<T>(string url, object body);
        Task<FetchResult<T>> GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders);
        Task<FetchResult<T>> PostAsync<T>(string url);
        Task<FetchResult<T>> PostAsync<T>(string url, object body);
        Task<FetchResult<T>> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders);
        Task<FetchResult<T>> PutAsync<T>(string url);
        Task<FetchResult<T>> PutAsync<T>(string url, object body);
        Task<FetchResult<T>> PutAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders);
    }
}
