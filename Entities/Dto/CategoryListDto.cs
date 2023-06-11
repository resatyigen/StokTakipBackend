using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Entities.Dto
{
    public class CategoryListDto
    {
        public List<Category> CategoryList { get; set; }
        public int ListSize { get; set; }
    }
}