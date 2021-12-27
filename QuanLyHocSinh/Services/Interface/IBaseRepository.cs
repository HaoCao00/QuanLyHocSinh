using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IBaseRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Object id);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAllAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Object id);
        Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> filter);
    }
}
