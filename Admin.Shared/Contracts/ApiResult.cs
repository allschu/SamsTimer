using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Contracts
{
    public class ApiResult<T>
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
    }
}
