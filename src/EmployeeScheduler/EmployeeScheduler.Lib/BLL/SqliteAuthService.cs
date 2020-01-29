using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.DTO;
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
        private const int _tokenLifeInSeconds = 60 * 60 * 24 * 30;

        private readonly string _adminPassword;
        private readonly string _userPassword;
        private readonly IClaimsService _claimsService;

        public SqliteAuthService(string adminPassword, string userPassword, IClaimsService claimsService)
        {
            _adminPassword = adminPassword;
            _userPassword = userPassword;
            _claimsService = claimsService;
        }

        public async Task<ClientToken> GetTokenAsync(string ipAddress, string password)
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

            var tokenValue = _claimsService.EncodeClaims(data, password);

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

            return new ClientToken
            {
                Token = tokenValue,
                Expires = expiration,
                Role = type
            };
        }

        private bool AdminPasswordIsValid(string password)
            => _adminPassword == password;

        private bool UserPasswordIsValid(string password)
            => _userPassword == password;

        public async Task<bool> ValidateTokenAsync(string ipAddress, string token, params Roles[] roles)
        {
            if (string.IsNullOrEmpty(token)) return false;

            using var context = new SchedulerContext();
            var existingToken = await context.Tokens.AsAsyncEnumerable().SingleOrDefaultAsync(t => t.TokenValue == token && t.IpAddress == ipAddress);
            if (existingToken == null) return false;
            if (existingToken.TokenValue != token) return false;
            if (existingToken.IpAddress != ipAddress) return false;

            foreach (var role in roles)
            {
                var password = GetPasswordForRole(role);
                if (ValidateToken(token, existingToken, password, role)) return true;
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
            try
            {
                var data = _claimsService.DecodeClaims(token, password);

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
            catch
            {
                return false;
            }
        }
    }
}
