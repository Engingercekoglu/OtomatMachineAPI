using OtomatMachine.Bussiness.Repositories.BookCheckOutHistoryRepository;
using OtomatMachine.Bussiness.Repositories.Person;
using OtomatMachine.Bussiness.Services.PersonService;
using System.Threading.Tasks;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.BookCheckOutHistoryService
{
    public class BookCheckOutHistoryService : IBookCheckOutHistoryService
    {
        private readonly IBookCheckOutHistoryRepository _bookCheckOutHistoryServiceRepository;

        public BookCheckOutHistoryService(IBookCheckOutHistoryRepository bookCheckOutHistoryServiceRepository)
        {
            _bookCheckOutHistoryServiceRepository = bookCheckOutHistoryServiceRepository;
        }
        public async Task<EF.Prouduct> AddAsync(EF.Prouduct bookCheckOutHistory)
        {
            return await _bookCheckOutHistoryServiceRepository.Add(bookCheckOutHistory);
        }

        public async Task<EF.Prouduct> UpdateAsync(EF.Prouduct bookCheckOutHistory)
        {
            return await _bookCheckOutHistoryServiceRepository.Update(bookCheckOutHistory);
        }

        public async Task<EF.Prouduct> Delete(EF.Prouduct bookCheckOutHistory)
        {
            return await _bookCheckOutHistoryServiceRepository.Delete(bookCheckOutHistory);
        }
    }
}
