using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync(int userID);
        Task<CategoryListDto> GetAllByFilter(int userID, string categoryName, Order order, int skip, int take);
        Task<List<Category>> GetAllWithProductAsync(int userID);
        Task<Category> AddAsync(Category category);
        Task<Category> GetAsync(int id);
        Task<Category> UpdateAsync(Category category);
        void Delete(Category category);
    }
}