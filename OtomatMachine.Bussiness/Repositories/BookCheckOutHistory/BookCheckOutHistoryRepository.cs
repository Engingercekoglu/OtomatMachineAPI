using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.BookCheckOutHistoryRepository
{
    public class BookCheckOutHistoryRepository : Repository<EF.Prouduct>, IBookCheckOutHistoryRepository
    {
        private readonly DataContext _dataContext;
        public BookCheckOutHistoryRepository(DataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
