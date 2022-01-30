using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.PaymentType
{
    public class PaymentTypeRepository : Repository<EF.PaymentType>, IPaymentTypeRepository
    {
        private readonly DataContext _dataContext;
        public PaymentTypeRepository(DataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
