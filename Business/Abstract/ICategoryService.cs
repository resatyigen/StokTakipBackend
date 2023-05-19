using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync(int userID);
        Task<List<Category>> GetAllWithProductAsync(int userID);
        Task<Category> AddAsync(Category category);
    }
}