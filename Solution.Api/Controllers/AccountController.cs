using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Solution.Api.StartupConfiguration;
using Solution.Application.Interfaces;
using Solution.Application.ViewModels;
using Solution.Domain.Notifications;
using System.Threading.Tasks;

namespace Solution.Api.Controllers
{
    [AllowAnonymous]
    [Route("v1/account")]
    [ApiController]     
    public class AccountController: ApiController
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(
            IAccountService authService, 
            ITokenService tokenService, 
            INotificationHandler<DomainNotification> notifications): base(notifications)
        {
            _accountService = authService;
            _tokenService = tokenService;
        }       

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel viewModel)
        {
            await _accountService.RegisterAsync(viewModel);
            return Response("Cadastro realizado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] IOptions<AppSettings> appSettings, [FromBody] UserLoginViewModel viewModel)
        {
            var user = await _accountService.LoginAsync(viewModel);

            if(user is null) return Response(user);

            return Response(new {
                Token = _tokenService.GenerateJWT(user, appSettings.Value.Secret),
                user.Id,
                user.Name,
                user.Email,
                user.Profile
            });                        
        }
        

        [HttpPut("change-Password")]
        public async Task<IActionResult> ResetPassowrd([FromBody] UserLoginResetViewModel viewModel)
        {
            var result = await _accountService.ChangePasswordAsync(viewModel);

            if (!result) return Response(result);

            return Response("Senha alterada com sucesso!");            
        }

    }
}
