using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface ISettingsService
    {
        Task<Lib.DAL.AdminSettings> GetSettings();
        Task<Lib.DAL.AdminSettings> SaveSettings(Lib.DAL.AdminSettings settings);
    }
}
