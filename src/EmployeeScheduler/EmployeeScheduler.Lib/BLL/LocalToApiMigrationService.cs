using Blazored.LocalStorage;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class LocalToApiMigrationService : IMigrationService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IFetchService _fetchService;

        public LocalToApiMigrationService(ILocalStorageService localStorage, IFetchService fetchService)
        {
            _localStorage = localStorage;
            _fetchService = fetchService;
        }

        public async Task MigrateAsync()
        {
            // TODO: This will migrate from LocalStorage to the Web API service.
        }
    }
}
