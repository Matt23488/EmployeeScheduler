using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeScheduler.Lib.BLL
{
    public class JavaScriptFetchService : IFetchService
    {
        private readonly IJSRuntime _js;
        private readonly ILogger _logger;

        private readonly Dictionary<string, Func<Task<string>>> _additionalHeaders;

        public JavaScriptFetchService(IJSRuntime js, ILogger logger)
        {
            _js = js;
            _logger = logger;
            _additionalHeaders = new Dictionary<string, Func<Task<string>>>();
        }

        public void AddAdditionalHeader(string name, Func<string> accessor)
        {
            _additionalHeaders[name] = () => Task.FromResult(accessor());
        }

        public void AddAdditionalHeader(string name, Func<Task<string>> accessor)
        {
            _additionalHeaders[name] = accessor;
        }

        public async Task<FetchResult<T>> GetAsync<T>(string url) => await GetAsync<T>(url, null, null);
        public async Task<FetchResult<T>> GetAsync<T>(string url, object body) => await GetAsync<T>(url, body, null);
        public async Task<FetchResult<T>> GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
        {
            var containsQueryStringAlready = url.Split("?").Length > 1;
            var properties = body?.GetType()?.GetProperties() ?? new System.Reflection.PropertyInfo[0];
            var newUrlBuilder = new StringBuilder(url);
            for (int i = 0; i < properties.Length; i++)
            {
                if (i == 0 && !containsQueryStringAlready) newUrlBuilder.Append("?");
                else newUrlBuilder.Append("&");

                newUrlBuilder.Append($"{HttpUtility.UrlEncode(properties[i].Name)}={Convert.ToString(properties[i].GetValue(body))}");
            }

            var data = new FetchData
            {
                Method = "GET"
            };
            return await FetchAsync<T>(data, newUrlBuilder.ToString(), additionalHeaders);
        }

        public async Task<FetchResult<T>> PostAsync<T>(string url) => await PostAsync<T>(url, null, null);
        public async Task<FetchResult<T>> PostAsync<T>(string url, object body) => await PostAsync<T>(url, body, null);
        public async Task<FetchResult<T>> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
        {
            var data = new FetchData
            {
                Method = "POST",
                Body = JsonConvert.SerializeObject(body)
            };
            return await FetchAsync<T>(data, url, additionalHeaders);
        }

        public async Task<FetchResult<T>> PutAsync<T>(string url) => await PutAsync<T>(url, null, null);
        public async Task<FetchResult<T>> PutAsync<T>(string url, object body) => await PutAsync<T>(url, body, null);
        public async Task<FetchResult<T>> PutAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
        {
            var data = new FetchData
            {
                Method = "PUT",
                Body = JsonConvert.SerializeObject(body)
            };
            return await FetchAsync<T>(data, url, additionalHeaders);
        }

        private async Task<FetchResult<T>> FetchAsync<T>(FetchData data, string url, Dictionary<string, string> additionalHeaders)
        {
            foreach (var kvp in _additionalHeaders)
            {
                data.Headers[kvp.Key] = await kvp.Value();
            }

            additionalHeaders?.ToList()?.ForEach(kvp => data.Headers[kvp.Key] = kvp.Value);

            var incompleteResult = await _js.InvokeAsync<IncompleteFetchResult>("blazorFetchService.fetch", url, data);
            var requestedData = default(T);
            try
            {
                requestedData = JsonConvert.DeserializeObject<T>(incompleteResult.Json.ToString());
            }
            catch { }
            return new FetchResult<T>
            {
                Status = incompleteResult.Status,
                Data = requestedData,
                UnParsedData = incompleteResult.Data
            };
        }

        private class FetchData
        {
            public string Method { get; set; }
            public string Cache { get; set; } = "no-cache";
            public object Body { get; set; }
            public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            };
        }

        private class IncompleteFetchResult
        {
            public int Status { get; set; }
            public object Data { get; set; }
            public JsonElement Json => (JsonElement)Data;
        }
    }
}
