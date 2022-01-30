using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.BookCheckOutHistoryService
{
    public interface IBookCheckOutHistoryService
    {
        Task<EF.Prouduct> AddAsync(EF.Prouduct bookCheckOutHistory);
        Task<EF.Prouduct> UpdateAsync(EF.Prouduct bookCheckOutHistory);
        Task<EF.Prouduct> Delete(EF.Prouduct bookCheckOutHistory);
    }
}
