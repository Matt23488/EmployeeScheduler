using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IAuthService
    {
        Task<DAL.Token> GetTokenAsync(string ipAddress, string password);
        Task<bool> ValidateTokenAsync(string ipAddress, string token);
    }
}
