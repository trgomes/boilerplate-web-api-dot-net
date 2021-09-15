using AutoMapper;
using MediatR;
using Solution.Application.Interfaces;
using Solution.Application.ViewModels;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using Solution.Domain.UseCases.AccountUseCases.AuthenticateAccount;
using Solution.Domain.UseCases.AccountUseCases.ChangeAccountPassword;
using Solution.Domain.UseCases.AccountUseCases.RegisterNewAccount;
using System.Threading.Tasks;

namespace Solution.Application.Services
{
    public class AccountService: IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountService(IMapper mapper, IMediator bus)
        {
            _mapper = mapper;
            _mediator = bus;
        }

        public Task<bool> RegisterAsync(UserRegisterViewModel vm)
        {
            var command = _mapper.Map<RegisterNewAccountCommand>(vm);
            return _mediator.Send(command);
        }

        public async Task<UserViewModel> LoginAsync(UserLoginViewModel vm)
        {
            var user = await _mediator.Send(new AuthenticateAccountCommand(vm.Email, vm.Password));
            return _mapper.Map<UserViewModel>(user);
        }        

        public Task<bool> ChangePasswordAsync(UserLoginResetViewModel vm)
        {
            return _mediator.Send(new ChangeAccountPasswordCommand(vm.Email, vm.OldPassword, vm.NewPassword));            
        }         
    }
}
