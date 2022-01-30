using OtomatMachine.Bussiness.Repositories.Base;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.Product
{
    public interface IProductRepository : IRepository<EF.Product>
    {
    }
}
