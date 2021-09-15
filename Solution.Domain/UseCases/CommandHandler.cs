using FluentValidation.Results;
using MediatR;
using Solution.Domain.Notifications;
using Solution.Domain.UoW;
using System.Threading.Tasks;

namespace Solution.Domain.UseCases
{
    public class CommandHandler
    {
        protected readonly IMediator _mediator;
        private readonly IUnitOfWork _uow;

        public CommandHandler(IUnitOfWork uow, IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }

        protected void NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.Publish(new DomainNotification("Error", error.ErrorMessage));
            }
        }

        protected void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        protected async Task<bool> Commit()
        {
            if (await _uow.Commit()) return true;

            await _mediator.Publish(new DomainNotification("Commit", "Ocorreu um problema ao salvar seus dados."));
            return false;
        }
    }
}
