﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IClaimsService
    {
        string EncodeClaims(Dictionary<string, object> claims, string key);
        Dictionary<string, object> DecodeClaims(string token, string key);
    }
}
