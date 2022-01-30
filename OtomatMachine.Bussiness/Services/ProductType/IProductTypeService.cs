using OtomatMachine.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.ProductType
{
    public interface IProductTypeService
    {
        Task<IDataResult<EF.ProductType>> AddAsync(EF.ProductType ProductType);
        Task<IDataResult<EF.ProductType>> DeleteAsync(int Id);
        Task<IDataResult<List<EF.ProductType>>> GetList(Status statusType);
    }
}
