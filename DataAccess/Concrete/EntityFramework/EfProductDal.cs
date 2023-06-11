using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Constants;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, StockTrackingContext>, IProductDal
    {
        public async Task<ProductListDto> GetAllByFilter(int? userId, int? categoryId, string productName, Order order, int skip, int take)
        {
            using (var context = new StockTrackingContext())
            {
                var query = context.Products.AsQueryable();
                query = query.Include(x => x.Category);

                if (userId != null)
                {
                    query = query.Where(x => x.Category.UserID == userId);
                }

                if (categoryId != null)
                {
                    query = query.Where(x => x.CategoryID == categoryId);
                }

                if (!string.IsNullOrEmpty(productName))
                {
                    query = query.Where(x => x.ProductName.Contains(productName));
                }

                if (order == Order.DESC)
                {
                    query = query.OrderByDescending(x => x.ID);
                }
                else
                {
                    query = query.OrderBy(x => x.ID);
                }

                int listSize = await query.CountAsync();

                query = query.Skip(skip).Take(take);

                List<Product> productList = await query.ToListAsync();

                return new ProductListDto
                {
                    ProductList = productList,
                    ListSize = listSize
                };
            }
        }

        public async Task<Product> GetWithCategory(int ID)
        {
            using (var context = new StockTrackingContext())
            {
                return await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ID == ID);
            }
        }
    }
}