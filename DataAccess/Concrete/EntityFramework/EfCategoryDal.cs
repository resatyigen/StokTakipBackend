using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, StockTrackingContext>, ICategoryDal
    {

        public async Task<List<Category>> GetAllWithProductAsync(int userId)
        {
            using (var context = new StockTrackingContext())
            {
                return await context.Categories.Include(x => x.Products).ToListAsync();
            }
        }
    }
}