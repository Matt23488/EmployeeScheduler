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
    public class JavaScriptConsoleLogger : ILogger
    {
        private readonly IJSRuntime _jsRuntime;

        public JavaScriptConsoleLogger(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task LogAsync(string message)
        {
            await _jsRuntime.InvokeAsync<object>("console.log", message);
        }

        public async Task LogAsync(params object[] stuff)
        {
            await _jsRuntime.InvokeAsync<object>("console.log", stuff);
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            //await _jsRuntime.InvokeAsync<object>("console.log", $"%cAn exception of type {ex.GetType().Name} was thrown:", ex.Message, ex.StackTrace, "color:red;");
            await _jsRuntime.InvokeAsync<object>("console.log", $"%cAn exception of type {ex.GetType().Name} was thrown:", "color:red;");
            await _jsRuntime.InvokeAsync<object>("console.log", $"%c{ex.Message}", "color:red;");
            await _jsRuntime.InvokeAsync<object>("console.log", $"%c{ex.StackTrace}", "color:red;");

            if (ex.InnerException != null)
            {
                await _jsRuntime.InvokeAsync<object>("console.log", "%cInner Exception:", "color:red;");
                await LogExceptionAsync(ex.InnerException);
            }
        }
    }
}
