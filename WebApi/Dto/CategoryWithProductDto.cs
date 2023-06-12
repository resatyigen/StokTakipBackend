using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{

    public class CategoryWithProductDto : CategoryDto
    {
        public CategoryWithProductDto()
        {
            Products = new List<ProductDto>();
        }

        public ICollection<ProductDto> Products { get; set; }
    }
}