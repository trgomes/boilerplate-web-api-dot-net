using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Solution.Api.StartupConfiguration;
using System.Text;

namespace Solution.Api.Configurations.Setup
{
    public static class AuthorizationService
    {
        public static void AddAuthorizationService(this IServiceCollection services, AppSettings appSettings, string defaultScheme)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var jwtPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(defaultScheme)
                        .RequireAuthenticatedUser()
                        .Build();

            services
                .AddAuthorization(auth =>
                {
                    auth.AddPolicy(defaultScheme, jwtPolicy);
                })
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = defaultScheme;
                    options.DefaultAuthenticateScheme = defaultScheme;
                    options.DefaultForbidScheme = defaultScheme;
                    options.DefaultSignInScheme = defaultScheme;
                    options.DefaultSignOutScheme = defaultScheme;
                    options.DefaultChallengeScheme = defaultScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
    }
}
