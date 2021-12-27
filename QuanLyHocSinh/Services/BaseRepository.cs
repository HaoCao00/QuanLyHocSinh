using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        readonly IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        private DbSet<T> _dbset;
        public BaseRepository(IDbContextFactory<QuanLyHocSinhContext> context)
        {

            _contextFactory = context ??
                throw new ArgumentNullException(nameof(context));
            _dbset = _contextFactory.CreateDbContext().Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            _dbset.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddAllAsync(List<T> entities)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            _dbset.AddRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(Object id)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            var entity = _dbset.Find(id);
            _dbset.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            return await _dbset.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            _dbset.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> filter)
        {
            var context = _contextFactory.CreateDbContext();
            _dbset = context.Set<T>();
            return await _dbset.Where(filter).ToListAsync();
        }
    }
}
