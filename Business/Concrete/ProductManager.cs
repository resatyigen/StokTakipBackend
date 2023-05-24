using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Task<Product> AddAsync(Product product)
        {
            return _productDal.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public Task<List<Product>> GetAllByCategoryIDAsync(int categoryID)
        {
            return _productDal.GetAllAsync(x => x.CategoryID == categoryID);
        }

        public Task<List<Product>> GetAllByUserIDAsync(int userID)
        {
            return _productDal.GetAllAsync(x => x.Category.UserID == userID);
        }

        public Task<Product> GetAsync(int id)
        {
            return _productDal.GetAsync(x => x.ID == id);
        }

        public Task<Product> GetWithCategoryAsync(int id)
        {
            return _productDal.GetWithCategory(id);
        }

        public Task<Product> UpdateAsync(Product product)
        {
            return _productDal.UpdateAsync(product);
        }
    }
}