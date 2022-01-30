using OtomatMachine.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.PaymentType
{
    public interface IPaymentTypeService
    {
        Task<IDataResult<EF.PaymentType>> AddAsync(EF.PaymentType paymentType);
        Task<IDataResult<EF.PaymentType>> DeleteAsync(int Id);
        Task<IDataResult<List<EF.PaymentType>>> GetList(Status statusType);
    }
}
