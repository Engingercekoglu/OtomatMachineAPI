using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.PaymentType
{
    public interface IPaymentTypeRepository : IRepository<EF.PaymentType>
    {
    }
}
