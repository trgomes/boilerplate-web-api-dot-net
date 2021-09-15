using MediatR;
using Solution.Domain.Models;

namespace Solution.Domain.UseCases.AccountUseCases.AuthenticateAccount
{
    public class AuthenticateAccountCommand : IRequest<User>
    {
        public AuthenticateAccountCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
