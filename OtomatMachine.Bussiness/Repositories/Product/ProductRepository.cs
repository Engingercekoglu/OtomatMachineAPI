using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.Product
{
    public class ProductRepository : Repository<EF.Product>, IProductRepository
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
