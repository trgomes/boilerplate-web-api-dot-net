using System.Threading.Tasks;

namespace Solution.Domain.UoW
{
    public interface IUnitOfWorkBase
    {
        void BeginTransaction();
        Task<bool> Commit();
        Task Rollback();
    }
}
