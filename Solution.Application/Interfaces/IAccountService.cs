using Solution.Application.ViewModels;
using System.Threading.Tasks;

namespace Solution.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(UserRegisterViewModel viewModel);
        Task<UserViewModel> LoginAsync(UserLoginViewModel viewModel);        
        Task<bool> ChangePasswordAsync(UserLoginResetViewModel viewModel);        
    }
}
