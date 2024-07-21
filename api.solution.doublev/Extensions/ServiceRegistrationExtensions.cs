using api.solution.doublev.Services.ServiceInterface;
using api.solution.doublev.Services.Services;

namespace api.solution.doublev.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddSingleton<ITokenService>(new TokenService("d7e4e9a5c8f53c0e32aee6f51b5b7d930d1e0b7bde90c2c2d3a8b5d6268ed855"));
    }
}