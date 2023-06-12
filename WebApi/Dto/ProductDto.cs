using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class ProductDto
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? ProductUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
    }
}