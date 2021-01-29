using Microsoft.Extensions.DependencyInjection;
using Solution.Api.StartupConfiguration;
using Solution.Infra.CrossCuting.IoC;
using System;

namespace Solution.Api.Configurations.Setup
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services, AppSettings appSettings)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjector.RegisterServices(services, appSettings);
        }
    }
}
