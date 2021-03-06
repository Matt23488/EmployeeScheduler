﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface ILogger
    {
        Task LogAsync(string message);
        Task LogAsync(params object[] stuff);
        Task LogExceptionAsync(Exception ex);
    }
}
