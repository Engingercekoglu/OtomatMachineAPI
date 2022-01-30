using OtomatMachine.Entity.Dtos;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.Product
{
    public interface IProductService
    {
        Task<IDataResult<EF.Product>> AddAsync(EF.Product Product);
        Task<IDataResult<EF.Product>> DeleteAsync(int Id);
        Task<IDataResult<List<EF.Product>>> GetList(ProductListDTO productList);
    }
}
