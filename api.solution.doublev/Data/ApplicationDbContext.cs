
namespace api.solution.doublev.Data;
using api.solution.doublev.Models.Auth;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users", "Auth");
    }
}