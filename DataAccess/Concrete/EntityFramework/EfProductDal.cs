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
    public class EfProductDal : EfEntityRepositoryBase<Product, StockTrackingContext>, IProductDal
    {
        public async Task<Product> GetWithCategory(int ID)
        {
            using (var context = new StockTrackingContext())
            {
                return await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ID == ID);
            }
        }
    }
}