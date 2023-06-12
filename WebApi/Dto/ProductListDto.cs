using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace WebApi.Dto
{
    public class ProductListDto
    {
        public ProductListDto()
        {
            ProductList = new List<ProductWithCategoryDto>();
        }

        public List<ProductWithCategoryDto> ProductList { get; set; }
        public int ListSize { get; set; }
    }
}