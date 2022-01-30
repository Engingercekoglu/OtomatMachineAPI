using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.ReceiptTransaction
{
    public interface IReceiptTransactionRepository : IRepository<EF.ReceiptTransaction>
    {
    }
}
