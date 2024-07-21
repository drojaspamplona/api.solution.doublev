using api.solution.doublev.Models.Person;

namespace api.solution.doublev.Services.ServiceInterface;

public interface IPersonService
{
    Task<IEnumerable<VwPerson>> GetAllPersonsAsync();
    Task<Person> GetPersonByIdAsync(int id);
    Task<int>  AddPersonAsync(CreatePerson persona);
    Task UpdatePersonAsync(Person persona);
    Task DeletePersonAsync(int id);
}