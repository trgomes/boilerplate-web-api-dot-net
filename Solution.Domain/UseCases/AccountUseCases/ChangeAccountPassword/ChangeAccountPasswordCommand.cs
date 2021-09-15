using MediatR;

namespace Solution.Domain.UseCases.AccountUseCases.ChangeAccountPassword
{
    public class ChangeAccountPasswordCommand : IRequest<bool>
    {        
        public string Email { get; private set; }
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }

        public ChangeAccountPasswordCommand(string email, string oldPassword, string newPassword)
        {
            Email = email;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
