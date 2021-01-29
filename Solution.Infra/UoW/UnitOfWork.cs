using Solution.Domain.UoW;
using Solution.Infra.Context;

namespace Solution.Infra.UoW
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(IDataContext dataContext) : base(dataContext) { }
    }
}
