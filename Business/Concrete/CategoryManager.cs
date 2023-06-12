using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public Task<Category> AddAsync(Category category)
        {
            return _categoryDal.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public Task<List<Category>> GetAllAsync(int userID)
        {
            return _categoryDal.GetAllAsync(x => x.UserID == userID);
        }

        public Task<CategoryListDto> GetAllByFilter(int userID, string? categoryName, Order order, int skip, int take)
        {
            return _categoryDal.GetAllByFilter(userID, categoryName, order, skip, take);
        }

        public Task<List<Category>> GetAllWithProductAsync(int userID)
        {
            return _categoryDal.GetAllWithProductAsync(userID);
        }

        public Task<Category> GetAsync(int id)
        {
            return _categoryDal.GetAsync(x => x.ID == id);
        }

        public Task<Category> UpdateAsync(Category category)
        {
            return _categoryDal.UpdateAsync(category);
        }
    }
}