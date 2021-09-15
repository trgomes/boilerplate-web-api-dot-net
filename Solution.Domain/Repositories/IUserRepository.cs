using Solution.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Solution.Domain.Repositorie
{
    public interface IUserRepository
    {
        Task<int> AddUser(User user);
        Task<User> FindUserByEmailAsync(string email);
        Task<int> UpdateUser(User user);
        Task<int> DeleteUser(Guid userId);
    }
}
