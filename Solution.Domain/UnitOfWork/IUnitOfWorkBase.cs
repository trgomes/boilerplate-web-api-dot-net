using System.Threading.Tasks;

namespace Solution.Domain.UoW
{
    public interface IUnitOfWorkBase
    {
        void Dispose();
        Task<bool> Commit();
        Task Rollback();
    }
}
