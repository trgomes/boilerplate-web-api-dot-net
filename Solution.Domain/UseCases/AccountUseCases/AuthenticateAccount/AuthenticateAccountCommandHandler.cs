using MediatR;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.Domain.UseCases.AccountUseCases.AuthenticateAccount
{
    public class AuthenticateAccountCommandHandler : CommandHandler, IRequestHandler<AuthenticateAccountCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateAccountCommandHandler(IMediator mediator, IUnitOfWork uow, IUserRepository userRepository) : base(uow, mediator)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AuthenticateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserByEmailAsync(request.Email);

            if (user == null)
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Não existe uma conta cadastrada para o email fornecido."));
            }

            if (!user.Authenticate(request.Email, request.Password))
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Email ou senha está(ão) incorreto(s)."));
            }

            return user;
        }
    }
}
