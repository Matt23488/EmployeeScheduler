using EmployeeScheduler.Lib.Services;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL.Web
{
    public class ToastService : IToastService
    {
        private IJSRuntime _jsRuntime;

        public ToastService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowAsync(string message, ToastType type)
        {
            await _jsRuntime.InvokeAsync<object>("window.toast.create", message, (int)type);
        }
    }
}
