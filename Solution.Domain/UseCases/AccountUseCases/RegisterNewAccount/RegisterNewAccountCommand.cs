using MediatR;

namespace Solution.Domain.UseCases.AccountUseCases.RegisterNewAccount
{
    public class RegisterNewAccountCommand : IRequest<bool>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public RegisterNewAccountCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
