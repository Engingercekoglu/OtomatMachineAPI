using OtomatMachine.Bussiness.Repositories.Category;
using OtomatMachine.Bussiness.Repositories.Person;
using OtomatMachine.Bussiness.Repositories.Product;
using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IPersonRepository _categoryRepository;
        private readonly IProductRepository _productRepository; 

        public CategoryService(
            IPersonRepository categoryRepository,
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<EF.Category> AddAsync(EF.Category category)
        {
            return await _categoryRepository.Add(category);
        }

        public async Task<EF.Category> UpdateAsync(EF.Category category)
        {
            return await _categoryRepository.Update(category);
        }

        public async Task<EF.Category> Delete(EF.Category category)
        {
            return await _categoryRepository.Delete(category);
        }
    }
}
