using FluentValidation.Results;
using Solution.Domain.Core.Bus;
using Solution.Domain.Notifications;
using Solution.Domain.UoW;
using System.Threading.Tasks;

namespace Solution.Application.Services
{
    public class Service
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _uow;

        public Service(IUnitOfWork uow, IMediatorHandler bus)
        {
            _uow = uow;
            _bus = bus;
        }

        protected void NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification("Error", error.ErrorMessage));
            }
        }

        protected async Task<bool> Commit()
        {
            if (await _uow.Commit()) return true;

            _ = _bus.RaiseEvent(new DomainNotification("Commit", "Ocorreu um problema ao salvar seus dados."));
            return false;
        }
    }
}
