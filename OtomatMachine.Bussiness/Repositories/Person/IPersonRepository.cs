using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.Person
{
    public interface IPersonRepository : IRepository<EF.ReciptTransaction>
    {
    }
}
