using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class SqliteAuthService : IAuthService
    {
        private readonly string _adminPassword;
        private readonly string _userPassword;
        private const int _tokenLifeInSeconds = 60 * 60 * 24 * 30;

        public SqliteAuthService(string adminPassword, string userPassword)
        {
            _adminPassword = adminPassword;
            _userPassword = userPassword;
        }

        public async Task<Token> GetTokenAsync(string ipAddress, string password)
        {
            var type = -1;
            if (AdminPasswordIsValid(password)) type = 0;
            else if (UserPasswordIsValid(password)) type = 1;
            else return null;

            var data = new Dictionary<string, object>
            {
                { "type", type }
            };

            var tokenValue = Jwt.JsonWebToken.Encode(data, password, Jwt.JwtHashAlgorithm.HS256);
            var token = new Token
            {
                IpAddress = ipAddress,
                Role = type,
                TokenValue = tokenValue,
                Expires = DateTime.Now.AddSeconds(_tokenLifeInSeconds)
            };

            using var context = new SchedulerContext();
            context.Tokens.Add(token);
            await context.SaveChangesAsync();

            return token;
        }

        private bool AdminPasswordIsValid(string password)
            => _adminPassword == password;

        private bool UserPasswordIsValid(string password)
            => _userPassword == password;

        public async Task<bool> ValidateTokenAsync(string ipAddress, string token)
        {
            using var context = new SchedulerContext();

            var existingToken = context.Tokens.SingleOrDefault(t => t.TokenValue == token && t.IpAddress == ipAddress);
            //var existingToken = context.Tokens.AsAsyncEnumerable().

            if (existingToken == null) return false;

            // TODO: Decode token and do validation

            return true;
        }
    }
}
