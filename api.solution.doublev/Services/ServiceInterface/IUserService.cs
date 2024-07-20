using api.solution.doublev.Models.Auth;

namespace api.solution.doublev.Services.ServiceInterface;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<string> AuthenticateAsync(string username, string password);
}