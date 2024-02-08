using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        internal DbSet<T> _dbSet;
        public BaseRepository(BlazorCARSDBContext blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
            _dbSet = _blazorDbContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter,
            bool tracked = true, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            //Handling record tracking
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            //Filteration
            query = query.Where(filter);

            //Inclusions
            if (includeProperties != null)
            {
                foreach (var includePro in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePro);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null, int pageSize = 100, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;

            //Filteration
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Pagination
            if (pageSize > 0)
            {
                int defaultpageSize = 100; // Read it from the app settings
                if (pageSize > defaultpageSize)
                {
                    pageSize = defaultpageSize;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            //Inclusions
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _dbSet.Update(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.AsNoTracking();
            return await query.AnyAsync(filter);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            return await query.CountAsync();
        }

        public async Task<int> MaxAsync()
        {
            IQueryable<T> query = _dbSet;
            return await query.CountAsync();
        }
    }
}
