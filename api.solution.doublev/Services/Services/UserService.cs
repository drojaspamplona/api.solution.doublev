using api.solution.doublev.Data;
using api.solution.doublev.Models.Auth;
using api.solution.doublev.Services.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace api.solution.doublev.Services.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public UserService(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username && u.UserPassWord == password);

        if (user == null)
        {
            return null;
        }

        return _tokenService.GenerateToken(user);
    }
}