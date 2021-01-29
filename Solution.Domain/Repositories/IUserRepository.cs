using Solution.Domain.Models;
using Solution.Domain.Repositories;
using System.Threading.Tasks;

namespace Solution.Domain.Repositorie
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> FindUserByEmailAsync(string email);
    }
}
