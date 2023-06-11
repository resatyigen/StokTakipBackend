using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class GetCategoryListDto
    {
        public string? CategoryName { get; set; }
        public string? Order { get; set; }
        public int RowsPerPage { get; set; }
        public int Page { get; set; }
    }
}