using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            Products = new Collection<Product>();
        }

        public int ID { get; set; }
        public int UserID { get; set; }
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public int RowIndex { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}