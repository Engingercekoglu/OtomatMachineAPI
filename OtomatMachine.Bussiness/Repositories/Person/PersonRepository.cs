using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.Person
{
    public class PersonRepository : Repository<EF.ReciptTransaction>, IPersonRepository
    {
        private readonly DataContext _dataContext;
        public PersonRepository(DataContext dataContext) 
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
