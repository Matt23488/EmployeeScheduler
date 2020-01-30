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

        public JavaScriptFetchService(IJSRuntime js, ILogger logger)
        {
            _js = js;
            _logger = logger;
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
                Method = "GET"
            };
            additionalHeaders?.ToList()?.ForEach(kvp => data.Headers[kvp.Key] = kvp.Value);

            return await _js.InvokeAsync<T>("blazorFetchService.fetch", newUrlBuilder.ToString(), data);
        }

        public async Task<T> PostAsync<T>(string url) => await PostAsync<T>(url, null, null);
        public async Task<T> PostAsync<T>(string url, object body) => await PostAsync<T>(url, body, null);
        public async Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders)
        {
            var data = new FetchData
            {
                Method = "POST",
                Body = JsonConvert.SerializeObject(body)
            };
            additionalHeaders?.ToList()?.ForEach(kvp => data.Headers[kvp.Key] = kvp.Value);

            return await _js.InvokeAsync<T>("blazorFetchService.fetch", url, data);
        }

        public async Task GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders, Func<T, Task> onSuccessAsync, Func<Task> onFailureAsync)
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
            additionalHeaders?.ToList()?.ForEach(kvp => data.Headers[kvp.Key] = kvp.Value);

            var result = await _js.InvokeAsync<IncompleteFetchResult>("blazorFetchService.fetch", newUrlBuilder.ToString(), data);
            if (result.Status == 200)
            {
                await onSuccessAsync(JsonConvert.DeserializeObject<T>(((JsonElement)result.Data).ToString()));
            }
            else await onFailureAsync();
        }

        public async Task PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders, Func<T, Task> onSuccessAsync, Func<Task> onFailureAsync)
        {
            var data = new FetchData
            {
                Method = "POST",
                Body = JsonConvert.SerializeObject(body)
            };
            additionalHeaders?.ToList()?.ForEach(kvp => data.Headers[kvp.Key] = kvp.Value);

            var result = await _js.InvokeAsync<IncompleteFetchResult>("blazorFetchService.fetch", url, data);
            if (result.Status == 200)
            {
                await onSuccessAsync(JsonConvert.DeserializeObject<T>(((JsonElement)result.Data).ToString()));
            }
            else await onFailureAsync();
        }

        public async Task PostAsync<T>(string url, object body, Func<T, Task> onSuccessAsync, Func<Task> onFailureAsync)
            => await PostAsync<T>(url, body, null, onSuccessAsync, onFailureAsync);

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
        }
    }
}
