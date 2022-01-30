using OtomatMachine.Bussiness.Repositories.Person;
using OtomatMachine.Bussiness.Services.PersonService;
using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace personsCheckinOutBussiness.Services.personService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<EF.ReciptTransaction> AddAsync(EF.ReciptTransaction person)
        {
            return await _personRepository.Add(person);
        }

        public async Task<EF.ReciptTransaction> UpdateAsync(EF.ReciptTransaction person)
        {
            return await _personRepository.Update(person);
        }

        public async Task<EF.ReciptTransaction> Delete(EF.ReciptTransaction person)
        {
            return await _personRepository.Delete(person);
        }
    }
}
