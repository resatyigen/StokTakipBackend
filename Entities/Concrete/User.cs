using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            Categories = new Collection<Category>();
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? PhotoPath { get; set; }
        public string Email { get; set; }
        public bool EmailValidated { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}