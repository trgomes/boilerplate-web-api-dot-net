using MediatR;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.Domain.UseCases.AccountUseCases.RegisterNewAccount
{
    public class RegisterNewAccountCommandHandler : CommandHandler, IRequestHandler<RegisterNewAccountCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public RegisterNewAccountCommandHandler(IMediator mediator, IUnitOfWork uow, IUserRepository userRespository) : base(uow, mediator)
        {
            _userRepository = userRespository;
        }

        public async Task<bool> Handle(RegisterNewAccountCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);

            var userValidate = user.Validate();

            if (!userValidate.IsValid)
            {
                NotifyValidationErrors(userValidate);
                return false;
            }

            var registredUser = await _userRepository.FindUserByEmailAsync(request.Email);

            if (registredUser != null)
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Já existe uma conta com esse email."));
                return false;
            }

            BeginTransaction();

            var result = await _userRepository.AddUser(user);

            if (result == 0)
            {
                await _mediator.Publish(new DomainNotification(string.Empty, "Não foi possivel criar a conta"));
                return false;
            }

            return await Commit();
        }
    }
}
