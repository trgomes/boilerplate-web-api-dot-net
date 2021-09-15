using Dapper;
using Solution.Domain.Models;
using Solution.Domain.Repositorie;
using Solution.Infra.Context;
using System;
using System.Threading.Tasks;

namespace Solution.Infra.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(IDataContext context): base(context) { }

        public Task<int> AddUser(User user)
        {
            var sql = @"
                INSERT INTO users (name, email, password, profile, active)
                VALUES (@name, @email, @password, @profile, @active)
            ";

            return _context.Connection.ExecuteAsync(sql, user, _context.Transaction);
        }        

        public Task<User> FindUserByEmailAsync(string email)
        {
            var sql = "SELECT * FROM users WHERE email = @email";

            return _context.Connection.QueryFirstOrDefaultAsync<User>(sql, new { email }, _context.Transaction);
        }

        public Task<int> UpdateUser(User user)
        {
            var sql = @"
                UPDATE users 
                SET
                    name = @name,
                    email = @email,
                    password = @password,
                    profile = @profile
                WHERE id = @id           
            ";

            return _context.Connection.ExecuteAsync(sql, user, _context.Transaction);
        }

        public Task<int> DeleteUser(Guid userId)
        {
            var sql = "DELETE FROM users WHERE id = @id";
            return _context.Connection.ExecuteAsync(sql, new { id = userId }, _context.Transaction);
        }
    }
}
