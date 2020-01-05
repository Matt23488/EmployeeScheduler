using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IToastService
    {
        Task Show(string message, ToastType type);
    }

    public enum ToastType
    {
        Info = 0,
        Success,
        Warning,
        Error
    }
}
