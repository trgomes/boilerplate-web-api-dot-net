using MediatR;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.Domain.UseCases.AccountUseCases.ChangeAccountPassword
{
    public class ChangeAccountPasswordCommandHandler : CommandHandler, IRequestHandler<ChangeAccountPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeAccountPasswordCommandHandler(IMediator mediator, IUnitOfWork uow, IUserRepository userRepository) : base(uow, mediator)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserByEmailAsync(request.Email);

            if (user == null)
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Não existe uma conta cadastrada para o email fornecido."));
                return false;
            }

            if (!user.Authenticate(request.Email, request.OldPassword))
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Email ou asenha atual está(ão) incorreto(s)."));
                return false;
            }

            BeginTransaction();

            var result = await _userRepository.UpdateUser(new User(user.Id, user.Name, user.Email, request.NewPassword, user.Profile, user.Active));

            if (result == 0)
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Não foi possível alterar a senha."));
                return false;
            }

            return await Commit();
        }
    }
}
