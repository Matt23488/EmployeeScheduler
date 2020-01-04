using EmployeeScheduler.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IAlertService
    {
        Task QueueInfoAlertAsync(string text);
        Task QueueSuccessAlertAsync(string text);
        Task QueueWarningAlertAsync(string text);
        Task QueueErrorAlertAsync(string text);
        Task<List<Alert>> GetAlertsAsync();
    }
}
