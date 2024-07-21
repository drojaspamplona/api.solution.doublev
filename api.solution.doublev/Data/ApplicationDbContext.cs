
using System.Data;
using api.solution.doublev.Models.Person;
using Microsoft.Data.SqlClient;

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
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users", "Auth");
        modelBuilder.Entity<Person>()
            .ToTable("Persons", "Persons")
            .HasKey(p => p.IdPerson);
    }
    
    public async Task<List<VwPerson>> GetPersonsAsync()
    {
        var persons = new List<VwPerson>();
        await using (var connection = (SqlConnection)Database.GetDbConnection())
        {
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "GetPersons";
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        persons.Add(new VwPerson
                        {
                            IdPerson = reader.GetInt32(reader.GetOrdinal("IdPerson")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            IdentificationNumber = reader.GetString(reader.GetOrdinal("IdentificationNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            IdentificationType = reader.GetString(reader.GetOrdinal("IdentificationType")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                            FullIdentification = reader.GetString(reader.GetOrdinal("FullIdentification")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        });
                    }
                }
            }
        }

        return persons;
    }
    
}