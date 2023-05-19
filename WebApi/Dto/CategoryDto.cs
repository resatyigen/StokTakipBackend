using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public int RowIndex { get; set; }
        public DateTime CreateDate { get; set; }
    }
}