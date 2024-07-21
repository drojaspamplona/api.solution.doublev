using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.solution.doublev.Data;
using api.solution.doublev.Models;
using api.solution.doublev.Models.Person;
using api.solution.doublev.Services.ServiceInterface;

namespace api.solution.doublev.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VwPerson>> GetAllPersonsAsync()
        {
            return await _context.GetPersonsAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<int> AddPersonAsync(CreatePerson createPerson)
        {
            var person = new Person
            {
                FirstName = createPerson.FirstName,
                LastName = createPerson.LastName,
                IdentificationNumber = createPerson.IdentificationNumber,
                Email = createPerson.Email,
                IdentificationType = createPerson.IdentificationType,
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person.IdPerson; 
        }


        public async Task UpdatePersonAsync(Person persona)
        {
            _context.Persons.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var persona = await _context.Persons.FindAsync(id);
            if (persona != null)
            {
                _context.Persons.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }
    }
}