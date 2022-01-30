using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<EF.Category> AddAsync(EF.Category category);
        Task<EF.Category> UpdateAsync(EF.Category category);
        Task<EF.Category> Delete(EF.Category category);
    }
}
