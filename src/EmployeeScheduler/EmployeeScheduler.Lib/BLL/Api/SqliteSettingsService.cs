using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL.Api
{
    public class SqliteSettingsService : ISettingsService
    {
        public async Task<AdminSettings> GetSettingsAsync()
        {
            using var context = new SchedulerContext();
            var settings = await context.AdminSettings.AsAsyncEnumerable().SingleOrDefaultAsync() ?? new AdminSettings();
            return settings;
        }

        public async Task<AdminSettings> SaveSettingsAsync(AdminSettings settings)
        {
            using var context = new SchedulerContext();
            var set = context.AdminSettings.AsAsyncEnumerable();
            if (await set.CountAsync() == 0) context.AdminSettings.Add(settings);
            else
            {
                var existing = await set.SingleOrDefaultAsync();
                existing.TimeZoneOffset = settings.TimeZoneOffset;
                existing.WeekStartOffset = settings.WeekStartOffset;
            }

            await context.SaveChangesAsync();
            return settings;
        }
    }
}
