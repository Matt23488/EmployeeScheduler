using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class ClientToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DAL.Roles Role { get; set; }
    }
}
