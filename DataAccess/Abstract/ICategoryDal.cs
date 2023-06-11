using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        Task<List<Category>> GetAllWithProductAsync(int userId);

        Task<CategoryListDto> GetAllByFilter(int userId, string categoryName, Order order, int skip, int take);
    }
}