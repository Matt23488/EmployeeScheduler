using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IPasswordValidationService
    {
        bool AdminPasswordIsValid(string password);
        bool UserPasswordIsValid(string password);
    }
}
