using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IMigrationService
    {
        Task MigrateAsync();
    }
}
