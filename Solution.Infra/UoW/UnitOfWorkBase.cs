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

        public Task<bool> Commit()
        {
            var success = false;

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

            return Task.FromResult(success);
        }

        public Task Rollback()
        {
            _dataContext.Transaction?.Rollback();
            _dataContext.Connection?.Close();
            _dataContext.Transaction?.Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }        
    }
}
