using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class PasswordValidationService : IPasswordValidationService
    {
        private readonly string _adminPassword;
        private readonly string _userPassword;

        public PasswordValidationService(string adminPassword, string userPassword)
        {
            _adminPassword = adminPassword;
            _userPassword = userPassword;
        }

        public bool AdminPasswordIsValid(string password)
            => _adminPassword == password;

        public bool UserPasswordIsValid(string password)
            => _userPassword == password;
    }
}
