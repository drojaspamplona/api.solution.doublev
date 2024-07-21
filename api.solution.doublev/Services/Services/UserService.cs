using System.Security.Cryptography;
using System.Text;
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
        var hashedPassword = HashPassword(password);
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username && u.UserPassWord == hashedPassword);

        if (user == null)
        {
            return null;
        }

        return _tokenService.GenerateToken(user);
    }
    
    public async Task<User> CreateUserAsync(User request)
    {
        var hashedPassword = HashPassword(request.UserPassWord);

        var user = new User
        {
            UserName = request.UserName,
            UserPassWord = hashedPassword,
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
    
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}