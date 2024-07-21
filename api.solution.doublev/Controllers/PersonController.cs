using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.solution.doublev.Models;
using api.solution.doublev.Models.Person;
using api.solution.doublev.Services.ServiceInterface;

namespace api.solution.doublev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwPerson>>> GetPersons()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(CreatePerson createPerson)
        {
            var idPerson = await _personService.AddPersonAsync(createPerson);
            var person = await _personService.GetPersonByIdAsync(idPerson);

            return CreatedAtAction(nameof(GetPerson), new { id = person.IdPerson }, person);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.IdPerson)
            {
                return BadRequest();
            }

            try
            {
                await _personService.UpdatePersonAsync(person);
            }
            catch (Exception exception)
            {
                if (await _personService.GetPersonByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personService.DeletePersonAsync(id);
            return NoContent();
        }
    }
}
