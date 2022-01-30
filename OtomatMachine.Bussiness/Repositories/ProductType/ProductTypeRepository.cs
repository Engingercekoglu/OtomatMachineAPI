using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Entity.Context;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Repositories.ProductType
{
    public class ProductTypeRepository : Repository<EF.ProductType>, IProductTypeRepository
    {
        private readonly DataContext _dataContext;
        public ProductTypeRepository(DataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
