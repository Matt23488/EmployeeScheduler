using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class FetchResult<T>
    {
        public int Status { get; set; }
        public T Data { get; set; }
        public object UnParsedData { get; set; }
    }
}
