using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllByCategoryIDAsync(int categoryID);
        Task<List<Product>> GetAllByUserIDAsync(int userID);
        Task<Product> AddAsync(Product product);
        Task<Product> GetAsync(int ID);
        Task<Product> GetWithCategoryAsync(int ID);
        Task<Product> UpdateAsync(Product product);
        void Delete(Product product);

    }
}