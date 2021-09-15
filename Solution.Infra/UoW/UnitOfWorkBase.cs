using Solution.Domain.UoW;
using Solution.Infra.Context;
using System;
using System.Threading.Tasks;

namespace Solution.Infra.UoW
{
    public class UnitOfWorkBase : IUnitOfWorkBase, IDisposable
    {
        private readonly IDataContext _dataContext;

        public UnitOfWorkBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void BeginTransaction()
        {
            _dataContext.Transaction = _dataContext.Connection.BeginTransaction();
        }

        public Task<bool> Commit()
        {
            bool success;

            try
            {
                _dataContext.Transaction.Commit();
                success = true;
            }
            catch (Exception)
            {
                _dataContext.Transaction.Rollback();
                success = false;
            }

            Dispose();

            return Task.FromResult(success);
        }

        public Task Rollback()
        {
            _dataContext.Transaction?.Rollback();

            Dispose();

            return Task.CompletedTask;
        }

        public void Dispose() => _dataContext.Transaction?.Dispose();        
    }
}
