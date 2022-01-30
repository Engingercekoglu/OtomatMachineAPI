using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.ProductType
{
    public interface IProductTypeRepository : IRepository<EF.ProductType>
    {
    }
}
