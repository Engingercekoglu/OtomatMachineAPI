using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.ReceiptTransaction
{
    public class ReceiptTransactionRepository : Repository<EF.ReceiptTransaction>, IReceiptTransactionRepository
    {
        private readonly DataContext _dataContext;
        public ReceiptTransactionRepository(DataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
