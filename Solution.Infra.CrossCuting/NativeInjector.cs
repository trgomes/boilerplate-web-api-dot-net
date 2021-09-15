using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Solution.Application.Interfaces;
using Solution.Application.Services;
using Solution.Domain.Models;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using Solution.Domain.UseCases.AccountUseCases.AuthenticateAccount;
using Solution.Domain.UseCases.AccountUseCases.ChangeAccountPassword;
using Solution.Domain.UseCases.AccountUseCases.RegisterNewAccount;
using Solution.Infra.Context;
using Solution.Infra.Repositories;
using Solution.Infra.UoW;

namespace Solution.Infra.CrossCuting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, dynamic appSettings)
        {
            // Domain - Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Handlers
            services.AddScoped<IRequestHandler<RegisterNewAccountCommand, bool>, RegisterNewAccountCommandHandler>();
            services.AddScoped<IRequestHandler<AuthenticateAccountCommand, User>, AuthenticateAccountCommandHandler>();
            services.AddScoped<IRequestHandler<ChangeAccountPasswordCommand, bool>, ChangeAccountPasswordCommandHandler>();

            // Application 
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            // Infra - Data - Context
            services.AddScoped<IDataContext>( _ => new DataContext(appSettings.ConnectionString));

            // Infra - Data - UoW
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Infra - Data - Repositories
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
