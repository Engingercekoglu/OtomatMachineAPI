using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.PersonService
{
    public interface IPersonService
    {
        Task<EF.ReciptTransaction> AddAsync(EF.ReciptTransaction person);
        Task<EF.ReciptTransaction> UpdateAsync(EF.ReciptTransaction person);
        Task<EF.ReciptTransaction> Delete(EF.ReciptTransaction person);
    }
}
