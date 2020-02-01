using Blazored.SessionStorage;
using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL.Web
{
    public class SessionStorageAlertService : IAlertService
    {
        private const string KEY_ALERTS = "EmployeeScheduler_Alerts";

        private ISessionStorageService _sessionStorage;

        public SessionStorageAlertService(ISessionStorageService sessionStorageService, ISyncSessionStorageService syncSessionStorage)
        {
            _sessionStorage = sessionStorageService;

            // TODO: Add keys to localStorage if they don't exist
            var alerts = syncSessionStorage.GetItem<Alert[]>(KEY_ALERTS);
            if (alerts == null)
            {
                syncSessionStorage.SetItem(KEY_ALERTS, new Alert[0]);
            }
        }

        public async Task QueueAlert(Alert alert)
        {
            var alerts = (await _sessionStorage.GetItemAsync<Alert[]>(KEY_ALERTS)).ToList();
            alerts.Add(alert);

            await _sessionStorage.SetItemAsync(KEY_ALERTS, alerts.ToArray());
        }

        public async Task QueueAlert(string text, AlertType type)
            => await QueueAlert(new Alert { Text = text, Type = type });

        public async Task QueueInfoAlertAsync(string text)
            => await QueueAlert(text, AlertType.Info);

        public async Task QueueSuccessAlertAsync(string text)
            => await QueueAlert(text, AlertType.Success);

        public async Task QueueWarningAlertAsync(string text)
            => await QueueAlert(text, AlertType.Warning);

        public async Task QueueErrorAlertAsync(string text)
            => await QueueAlert(text, AlertType.Error);

        public async Task<List<Alert>> GetAlertsAsync()
        {
            var alerts = (await _sessionStorage.GetItemAsync<Alert[]>(KEY_ALERTS))
                .OrderByDescending(a => a.Type)
                .ToList();

            await _sessionStorage.SetItemAsync(KEY_ALERTS, new Alert[0]);

            return alerts;
        }
    }
}
