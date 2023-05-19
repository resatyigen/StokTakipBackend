using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

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

        public Task<List<Category>> GetAllAsync(int userID)
        {
            return _categoryDal.GetAllAsync(x => x.UserID == userID);
        }

        public Task<List<Category>> GetAllWithProductAsync(int userID)
        {
            return _categoryDal.GetAllWithProductAsync(userID);
            // return categoryList.Select(x => new Category()
            // {
            //     ID = x.ID,
            //     CategoryName = x.CategoryName,
            //     Description = x.Description,
            //     Color = x.Color,
            //     ImagePath = x.ImagePath,
            //     UserID = x.UserID,
            //     CreateDate = x.CreateDate,
            //     Products = x.Products.ToList()
            // }).ToList();

        }
    }
}