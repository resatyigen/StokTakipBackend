using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (TContext context = new TContext())//using içindeki context garbage collector yardımıyla belleği hızlıca temizler.Performans için yazdık.
            {
                var addedEntity = context.Entry(entity);

                addedEntity.State = EntityState.Added;//Ekleme işlemi yapılacağını bildirdik. 

                await context.SaveChangesAsync();//İşlemleri gerçekleştir.
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);

                deletedEntity.State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {           //filter boş mu     EVET ise bütün datayı döndür           HAYIR ise filtreyi uygula 
                return filter == null ? await context.Set<TEntity>().ToListAsync() : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);

                updatedEntity.State = EntityState.Modified;

                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}