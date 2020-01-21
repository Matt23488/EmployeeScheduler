using EmployeeScheduler.Lib.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public async Task<T> GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
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
                method = "GET"
            };
            additionalHeaders?.ToList()?.ForEach(kvp => data.headers[kvp.Key] = kvp.Value);

            return await _js.InvokeAsync<T>("blazorFetchService.fetch", newUrlBuilder.ToString(), data);
        }

        public async Task<T> PostAsync<T>(string url) => await PostAsync<T>(url, null, null);
        public async Task<T> PostAsync<T>(string url, object body) => await PostAsync<T>(url, body, null);
        public async Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
        {
            var data = new FetchData
            {
                method = "POST",
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
