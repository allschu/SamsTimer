using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared
{
    public class PagingParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
    }
}
