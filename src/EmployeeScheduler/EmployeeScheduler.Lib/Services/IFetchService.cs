﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface IFetchService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> GetAsync<T>(string url, object body);
        Task<T> GetAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders);
        Task<T> PostAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object body);
        Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> additionalHeaders);
    }
}