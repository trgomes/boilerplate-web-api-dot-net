using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Solution.Application.Interfaces;
using Solution.Application.Services;
using Solution.Domain.Core.Bus;
using Solution.Domain.Notifications;
using Solution.Domain.Repositorie;
using Solution.Domain.UoW;
using Solution.Infra.Context;
using Solution.Infra.CrossCuting.Bus;
using Solution.Infra.Repositories;
using Solution.Infra.UoW;

namespace Solution.Infra.CrossCuting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, dynamic appSettings)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands

            // Application 
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            // Infra - Data - Context
            services.AddScoped<IDataContext>( _ => new DataContext(appSettings.ConnectionString));

            // Infra - Data - UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data - Repositories
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
