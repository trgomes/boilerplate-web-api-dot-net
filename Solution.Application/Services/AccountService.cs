using AutoMapper;
using Solution.Application.Interfaces;
using Solution.Application.ViewModels;
using Solution.Domain.Core.Bus;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using System.Threading.Tasks;

namespace Solution.Application.Services
{
    public class AccountService: Service, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IUserRepository _userRepository;

        public AccountService(
            IMapper mapper,
            IMediatorHandler bus,
            IUnitOfWork uow,
            IUserRepository userRepository) : base(uow, bus)
        {
            _mapper = mapper;
            _bus = bus;
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(UserRegisterViewModel viewModel)
        {
            if ( await FindUserByEmailAsync(viewModel.Email) != null)
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Já existe uma conta com esse email."));
                return false;
            }

            var user = _mapper.Map<UserRegisterViewModel, User>(viewModel);

            var result = await _userRepository.Insert(user);

            if (result == 0)
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Não foi possivel criar a conta"));
                return false;
            }                

            return await Commit();
        }

        public async Task<UserViewModel> LoginAsync(UserLoginViewModel model)
        {
            var user = await FindUserByEmailAsync(model.Email);

            if (user == null)
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Não existe uma conta cadastrada para o email fornecido."));
                return null;
            }

            if (!user.Authenticate(model.Email, model.Password))
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Email ou senha está(ão) incorreto(s)."));
                return null;
            }                

            return _mapper.Map<UserViewModel>(user);
        }        

        public async Task<bool> ChangePasswordAsync(UserLoginResetViewModel model)
        {
            var user = await FindUserByEmailAsync(model.Email);

            if(user == null)
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Não existe uma conta cadastrada para o email fornecido."));
                return false;
            }

            if (!user.Authenticate(model.Email, model.OldPassword))
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Email ou asenha atual está(ão) incorreto(s)."));
                return false;
            }                

            var result = await _userRepository.Update(new User(user.Id, user.Name, user.Email, model.NewPassword, user.Profile, user.Active));

            if (!result)
            {
                _ = _bus.RaiseEvent(new DomainNotification(string.Empty, "Não foi possível alterar a senha."));
                return false;
            }                

            return await Commit();
        }

        private async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userRepository.FindUserByEmailAsync(email);
        }           
    }
}
