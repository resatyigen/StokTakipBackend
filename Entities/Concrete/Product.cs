using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? ProductUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Category Category { get; set; }
    }
}