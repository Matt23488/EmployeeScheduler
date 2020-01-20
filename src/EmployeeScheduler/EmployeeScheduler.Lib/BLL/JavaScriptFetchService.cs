using EmployeeScheduler.Lib.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class JavaScriptFetchService : IFetchService
    {
        private IJSRuntime _js;

        public JavaScriptFetchService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<T> GetAsync<T>(string url) => await GetAsync<T>(url, null, null);
        public async Task<T> GetAsync<T>(string url, object body) => await GetAsync<T>(url, body, null);
        public async Task<T> GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders) => await DoRequestAsync<T>(url, body, additionalHeaders, "GET");

        public async Task<T> PostAsync<T>(string url) => await PostAsync<T>(url, null, null);
        public async Task<T> PostAsync<T>(string url, object body) => await PostAsync<T>(url, body, null);
        public async Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders) => await DoRequestAsync<T>(url, body, additionalHeaders, "POST");

        public async Task<T> DoRequestAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders, string method)
        {
            var data = new FetchData
            {
                method = method,
                body = JsonConvert.SerializeObject(body)
            };
            additionalHeaders?.ToList()?.ForEach(kvp => data.headers[kvp.Key] = kvp.Value);

            return await _js.InvokeAsync<T>("blazorFetchService.fetch", url, data);
        }

        private class FetchData
        {
            public string method { get; set; }
            public string cache { get; set; } = "no-cache";
            public object body { get; set; }
            public Dictionary<string, string> headers { get; set; } = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            };
        }
    }
}
