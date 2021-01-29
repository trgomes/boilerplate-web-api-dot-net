using Dapper;
using Solution.Domain.Models;
using Solution.Domain.Repositorie;
using Solution.Infra.Context;
using System.Threading.Tasks;

namespace Solution.Infra.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(IDataContext context): base(context) { }

        public Task<User> FindUserByEmailAsync(string email)
        {
            var sql =
                " SELECT * FROM users" +
                " WHERE email = @email";

            return _context.Connection.QueryFirstOrDefaultAsync<User>(sql, new { email }, _context.Transaction);
        }
    }
}
