using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null, params string[] includes);
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> filter);
        Task<int> SaveAsync();
        Task<T> GetById(int id);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
