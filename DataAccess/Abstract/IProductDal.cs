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
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<Product> GetWithCategory(int ID);
        Task<ProductListDto> GetAllByFilter(int? userId, int? categoryId, string productName, Order order, int skip, int take);
    }
}