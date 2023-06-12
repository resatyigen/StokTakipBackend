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
    public class EfCategoryDal : EfEntityRepositoryBase<Category, StockTrackingContext>, ICategoryDal
    {
        public async Task<CategoryListDto> GetAllByFilter(int userId, string? categoryName, Order order, int skip, int take)
        {
            using (var context = new StockTrackingContext())
            {
                var query = context.Categories.AsQueryable();
                query = query.Where(x => x.UserID == userId);
                if (!string.IsNullOrEmpty(categoryName))
                {
                    query = query.Where(x => x.CategoryName.Contains(categoryName));
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

                List<Category> categoryList = await query.ToListAsync();

                return new CategoryListDto
                {
                    CategoryList = categoryList,
                    ListSize = listSize
                };
            }
        }

        public async Task<List<Category>> GetAllWithProductAsync(int userId)
        {
            using (var context = new StockTrackingContext())
            {
                return await context.Categories.Include(x => x.Products).ToListAsync();
            }
        }
    }
}