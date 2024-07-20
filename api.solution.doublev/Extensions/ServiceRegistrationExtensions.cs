using api.solution.doublev.Services.ServiceInterface;
using api.solution.doublev.Services.Services;

namespace api.solution.doublev.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<ITokenService>(new TokenService("your_secret_key_here"));
    }
}