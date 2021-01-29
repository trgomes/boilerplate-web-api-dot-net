using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solution.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> Insert(T obj);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Update(T obj);
        Task<bool> Delete(T obj);
    }
    
}
