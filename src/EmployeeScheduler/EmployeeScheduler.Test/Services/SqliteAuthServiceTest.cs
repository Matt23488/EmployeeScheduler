using EmployeeScheduler.Lib.BLL.Api;
using EmployeeScheduler.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace EmployeeScheduler.Test.Services
{
    [TestClass]
    public class SqliteAuthServiceTest
    {
        private readonly IAuthService _authService;

        public SqliteAuthServiceTest()
        {
            _authService = new SqliteAuthService("adminPassword", "userPassword", new ClaimsService());
        }

        [TestMethod]
        public async Task ValidateTokenAsync_User()
        {
            var result = default(bool);
            var token = await _authService.GetTokenAsync("1.2.3.4", "userPassword");
            result = await _authService.ValidateTokenAsync("1.2.3.4", token.Token, Lib.DAL.Roles.User);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ValidateTokenAsync_Admin()
        {
            var result = default(bool);
            var token = await _authService.GetTokenAsync("1.2.3.4", "adminPassword");
            result = await _authService.ValidateTokenAsync("1.2.3.4", token.Token, Lib.DAL.Roles.Admin);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ValidateTokenAsync_Either()
        {
            var userResult = default(bool);
            var userToken = await _authService.GetTokenAsync("1.2.3.4", "userPassword");
            userResult = await _authService.ValidateTokenAsync("1.2.3.4", userToken.Token, Lib.DAL.Roles.User, Lib.DAL.Roles.Admin);

            var adminResult = default(bool);
            var adminToken = await _authService.GetTokenAsync("1.2.3.4", "adminPassword");
            adminResult = await _authService.ValidateTokenAsync("1.2.3.4", adminToken.Token, Lib.DAL.Roles.User, Lib.DAL.Roles.Admin);

            Assert.IsTrue(userResult);
            Assert.IsTrue(adminResult);
        }

        [TestMethod]
        public async Task ValidateTokenAsync_Invalid()
        {
            var userResult = default(bool);
            var userToken = await _authService.GetTokenAsync("1.2.3.4", "userPassword");
            userResult = await _authService.ValidateTokenAsync("1.2.3.4", userToken.Token, Lib.DAL.Roles.Admin);

            var adminResult = default(bool);
            var adminToken = await _authService.GetTokenAsync("1.2.3.4", "adminPassword");
            adminResult = await _authService.ValidateTokenAsync("1.2.3.4", adminToken.Token, Lib.DAL.Roles.User);

            Assert.IsFalse(userResult);
            Assert.IsFalse(adminResult);
        }
    }
}
