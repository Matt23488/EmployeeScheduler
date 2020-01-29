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

        public async Task<string> GetTokenAsync(string ipAddress, string password)
        {
            var type = Roles.None;
            if (AdminPasswordIsValid(password)) type = Roles.Admin;
            else if (UserPasswordIsValid(password)) type = Roles.User;
            else return null;

            var expiration = DateTime.Now.AddSeconds(_tokenLifeInSeconds);
            var data = new Dictionary<string, object>
            {
                { "type", (int)type },
                { "expires", expiration.Ticks }
            };

            var tokenValue = Jwt.JsonWebToken.Encode(data, password, Jwt.JwtHashAlgorithm.HS256);

            var token = new Token
            {
                IpAddress = ipAddress,
                Role = (int)type,
                TokenValue = tokenValue,
                Expires = expiration
            };

            using var context = new SchedulerContext();
            context.Tokens.Add(token);
            await context.SaveChangesAsync();

            return tokenValue;
        }

        private bool AdminPasswordIsValid(string password)
            => _adminPassword == password;

        private bool UserPasswordIsValid(string password)
            => _userPassword == password;

        public async Task<bool> ValidateTokenAsync(string ipAddress, string token, params Roles[] roles)
        {
            using var context = new SchedulerContext();
            var existingToken = await context.Tokens.AsAsyncEnumerable().SingleOrDefaultAsync(t => t.TokenValue == token && t.IpAddress == ipAddress);
            if (existingToken == null) return false;
            if (existingToken.TokenValue != token) return false;
            if (existingToken.IpAddress != ipAddress) return false;

            foreach (var role in roles)
            {
                var password = GetPasswordForRole(role);
                try
                {
                    if (ValidateToken(token, existingToken, password, role)) return true;
                }
                catch { }
            }

            return false;
        }

        private string GetPasswordForRole(Roles role)
        {
            switch (role)
            {
                case Roles.Admin: return _adminPassword;
                case Roles.User: return _userPassword;
                default: throw new ArgumentOutOfRangeException(nameof(role));
            }
        }

        private bool ValidateToken(string token, Token existingToken, string password, Roles role)
        {
            var data = default(Dictionary<string, object>);
            try
            {
                data = Jwt.JsonWebToken.DecodeToObject<Dictionary<string, object>>(token, password);
            }
            catch
            {
                return false;
            }

            if (!data.ContainsKey("type")) return false;
            if (!data.ContainsKey("expires")) return false;

            var type = Convert.ToInt32(data["type"]);
            if (type != (int)role) return false;
            if (type != existingToken.Role) return false;

            var expires = Convert.ToInt64(data["expires"]);
            if (expires != existingToken.Expires.Ticks) return false;
            if (expires < DateTime.Now.Ticks) return false;

            return true;
        }
    }
}
