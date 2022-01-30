using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.BookCheckOutHistoryRepository
{
    public interface IBookCheckOutHistoryRepository : IRepository<EF.Prouduct>
    {
    }
}
