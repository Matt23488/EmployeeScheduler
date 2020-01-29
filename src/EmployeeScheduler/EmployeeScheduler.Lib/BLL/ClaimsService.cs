using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class ClaimsService : IClaimsService
    {
        public string EncodeClaims(Dictionary<string, object> claims, string key)
            => Jwt.JsonWebToken.Encode(claims, key, Jwt.JwtHashAlgorithm.HS256);

        public Dictionary<string, object> DecodeClaims(string token, string key)
        {
            try
            {
                return Jwt.JsonWebToken.DecodeToObject<Dictionary<string, object>>(token, key);
            }
            catch
            {
                return null;
            }
        }
    }
}
