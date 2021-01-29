using Dapper.Contrib.Extensions;
using Solution.Domain.Repositories;
using Solution.Infra.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solution.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly IDataContext _context;

        public Repository(IDataContext dataContext)
        {
            _context = dataContext;
        }        

        public Task<int> Insert(T obj)
        {
            return _context.Connection.InsertAsync(obj, _context.Transaction);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _context.Connection.GetAllAsync<T>(_context.Transaction);
        }

        public Task<T> GetById(Guid id)
        {
            return _context.Connection.GetAsync<T>(id, _context.Transaction);
        }

        public Task<bool> Update(T obj)
        {
            return _context.Connection.UpdateAsync(obj, _context.Transaction);
        }

        public Task<bool> Delete(T obj)
        {
            return _context.Connection.DeleteAsync<T>(obj, _context.Transaction);
        }


    }
}
